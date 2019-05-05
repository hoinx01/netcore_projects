using Confluent.Kafka;
using Hinox.Static.Application;
using Hinox.Static.Constants;
using Hinox.Static.Utilities;
using NetCore.EventBus.Abstractions;
using NetCore.EventBus.Events;
using NetCore.Utils.Logging;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.EventBus.Kafka
{
    public class EventBusKafka : IEventBus, IDisposable
    {
        private readonly IEventBusSubscriptionsManager subscriptionsManager;
        private readonly KafkaConfig kafkaConfig;
        private readonly KafkaProducer kafkaProducer;
        private readonly Logger logger = NLogManager.GetCurrentClassLogger();

        public EventBusKafka(
            IEventBusSubscriptionsManager subscriptionsManager,
            KafkaConfig kafkaConfig,
            KafkaProducer kafkaProducer
            )
        {
            this.subscriptionsManager = subscriptionsManager;
            this.kafkaConfig = kafkaConfig;
            this.kafkaProducer = kafkaProducer;
        }

        public void Dispose()
        {

        }

        public async Task Publish<T>(T @event) where T : IntegrationEvent
        {
            string eventKey = @event.GetType().Name;

            string topic = kafkaConfig.TopicMap[eventKey];
            var message = new Message<string, string>()
            {
                Key = @event.Key,
                Value = JsonConvert.SerializeObject(@event, NewtonJsonSerializerSettings.SNAKE)
            };

            var deliveryReport = await kafkaProducer.ProduceAsync(topic, message);
            logger.Info(string.Format("topic: {0}, partition: {1}, offset: {2}", deliveryReport.Topic, deliveryReport.Partition, deliveryReport.Offset));
        }

        public async Task Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            string eventName = subscriptionsManager.GetEventKey<T>();
            var containsKey = subscriptionsManager.HasSubscriptionsForEvent(eventName);
            if (containsKey)
                return;

            subscriptionsManager.AddSubscription<T, TH>();

            await DoInternalSubscription<T, TH>(eventName);
        }
        private async Task DoInternalSubscription<T, TH>(string eventName)
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var subscriptionInfos = subscriptionsManager.GetHandlersForEvent(eventName).ToList();

            List<TH> handlers = new List<TH>();
            try
            {
                foreach(var subscriptionInfo in subscriptionInfos)
                {
                    var type = subscriptionInfo.HandlerType;
                    TH handler = (TH)DependencyManager.ServiceProvider.GetService(type);
                    handlers.Add(handler);
                }
            }
            catch (Exception e)
            {
                logger.Error(e);
                throw e;
            }

            await Task.Run(async () =>
            {
                var consumer = new KafkaConsumer(kafkaConfig);
                consumer.Subscribe(kafkaConfig.TopicMap[eventName]);
                bool consumming = true;

                consumer.OnError += (obj, error) =>
                {
                    logger.Error(error);
                    consumming = !error.IsFatal;
                };

                
                while (consumming)
                {
                    try
                    {
                        var message = consumer.Consume();
                        var @event = JsonConvert.DeserializeObject<T>(message.Value, NewtonJsonSerializerSettings.SNAKE);

                        logger.Info(string.Format("topic: {0}, partition: {1}, offset: {2}", message.Topic, message.Partition, message.Offset));

                        var tasks = handlers.Select(s => s.Handle(@event)).ToList();

                        try
                        {
                            await Task.WhenAll(tasks);
                        }
                        catch(Exception e)
                        {
                            throw;
                        }

                        consumer.StoreOffsets(new List<TopicPartitionOffset>() { new TopicPartitionOffset(message.TopicPartition, message.Offset) });
                        var commitedOffset = consumer.Commit(message);
                        //logger.Info(string.Format("topic: {0}, offset: {1} success", msg.Topic, msg.Offset));
                    }
                    catch (Exception e)
                    {
                        logger.Error(e);
                        //logger.Error(e, string.Format("topic: {0}, offset: {1} failed", msg.Topic, msg.Offset));
                        throw;
                    }
                }
            });
            
        }

        public async Task Subscribe(string eventTypeName, string subscriberGroup, int subscriberNumber, string handlerTypeName)
        {
            try
            {
                var originConsumerConfig = kafkaConfig.GetConsumerConfig();
                var consumerConfig = new Dictionary<string, string>();
                foreach (var key in originConsumerConfig.Keys)
                {
                    consumerConfig[key] = originConsumerConfig[key];
                }
                consumerConfig["group.id"] = subscriberGroup;

                var eventType = ObjectUtils.GetType(eventTypeName);

                var handlerType = ObjectUtils.GetType(handlerTypeName);
                var handlerInstance = (IIntegrationEventHandler)DependencyManager.ServiceProvider.GetService(handlerType);

                for (int consumerIndex = 0; consumerIndex < subscriberNumber; consumerIndex++)
                {
                    RegisterConsumer(eventTypeName, subscriberGroup, consumerConfig, eventType, handlerInstance, consumerIndex);
                }
            }
            catch(Exception e)
            {
                logger.Error(e);
                throw;
            }

        }

        private void RegisterConsumer(string eventTypeName, string subscriberGroup, Dictionary<string, string> consumerConfig, Type eventType, IIntegrationEventHandler handlerInstance, int consumerIndex)
        {
            Task.Run(async () =>
            {

                var consumer = new KafkaConsumer(consumerConfig);

                var eventNameKey = eventTypeName.Split(".").Last();
                string topic = kafkaConfig.TopicMap[eventNameKey];
                consumer.Subscribe(topic);
                bool consumming = true;

                consumer.OnError += (obj, error) =>
                {
                    logger.Error(error);
                    consumming = !error.IsFatal;
                };
                consumer.OnPartitionsAssigned += (obj, partitions) =>
                {
                    Console.WriteLine("Assigned; consumer_group:{0}[{1}], partition:{2}", subscriberGroup, consumerIndex, string.Join(',', partitions.Select(s => s.Partition.Value)));
                };
                consumer.OnPartitionsRevoked += (obj, partitions) =>
                {
                    Console.WriteLine("Revoked; consumer_group:{0}[{1}], partition:{2}", subscriberGroup, consumerIndex, string.Join(',', partitions.Select(s => s.Partition.Value)));
                };

                while (consumming)
                {
                    try
                    {
                        var message = consumer.Consume();

                        //Console.WriteLine("consumer_group:{0}[{1}], topic: {2}, partition: {3}, offset: {4}", subscriberGroup, consumerIndex, message.Topic, message.Partition, message.Offset);
                        //logger.Info(string.Format("topic: {0}, partition: {1}, offset: {2}", message.Topic, message.Partition, message.Offset));

                        var @event = JsonConvert.DeserializeObject(message.Value, eventType, NewtonJsonSerializerSettings.SNAKE);
                        await handlerInstance.Handle(@event);

                        consumer.StoreOffsets(new List<TopicPartitionOffset>() { new TopicPartitionOffset(message.TopicPartition, message.Offset) });
                        var commitedOffset = consumer.Commit(message);
                        //logger.Info(string.Format("topic: {0}, offset: {1} success", msg.Topic, msg.Offset));
                    }
                    catch (Exception e)
                    {
                        logger.Error(e);
                        //logger.Error(e, string.Format("topic: {0}, offset: {1} failed", msg.Topic, msg.Offset));
                        throw;
                    }
                }
            });
        }

        public async Task Unsubscribe<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent
        {
            subscriptionsManager.RemoveSubscription<T, TH>();
            await Task.CompletedTask;
        }
    }
}
