using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using NLog;

namespace NetCore.Queue.Kafka
{
    public class ConsumerWrapper
    {
        private KafkaConsumer consumer;
        private string[] topics;
        private BaseKafkaMessageProcessor processor;
        private Logger logger = LogManager.GetLogger("KafkaConsumerWrapper");

        public ConsumerWrapper(KafkaConsumer consumer, string[] topics, BaseKafkaMessageProcessor processor)
        {
            this.consumer = consumer;
            this.processor = processor;
            this.topics = topics;
        }
        public async Task ExecuteAsync()
        {
            await Task.Run(async () =>
            {
                consumer.Subscribe(topics);

                bool consumming = true;
                consumer.OnError += (obj, error) =>
                {
                    Console.WriteLine(error);
                    consumming = !error.IsFatal;
                };

                while (consumming)
                {
                    var msg = consumer.Consume();
                    try
                    {
                        logger.Info(string.Format("topic: {0}, offset: {1}", msg.Topic, msg.Offset));
                        await processor.ProcessMessageAsync(msg);
                        consumer.StoreOffsets(new List<TopicPartitionOffset>() { new TopicPartitionOffset(msg.TopicPartition, msg.Offset) });
                        var commitedOffset = consumer.Commit(msg);
                        logger.Info(string.Format("topic: {0}, offset: {1} success", msg.Topic, msg.Offset));
                    }
                    catch (Exception e)
                    {
                        logger.Error(e, string.Format("topic: {0}, offset: {1} failed", msg.Topic, msg.Offset));
                        throw;
                    }
                }
                
            });
        }
    }
}
