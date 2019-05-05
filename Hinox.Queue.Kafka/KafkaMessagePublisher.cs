using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using NLog;

namespace NetCore.Queue.Kafka
{
    public class KafkaMessagePublisher
    {
        private Producer<string,string> producer;
        private Logger logger = LogManager.GetLogger("KafkaMessagePublisher");

        public KafkaMessagePublisher(KafkaProducer producer)
        {
            this.producer = producer;
        }
        public async Task<DeliveryReport<string, string>> Publish(string topic, string payload)
        {
            try
            {
                var message = new Message<string, string>();
                message.Key = DateTime.Now.ToLongTimeString();
                message.Value = payload;
                var deliveryReport = await producer.ProduceAsync(topic, message);
                return deliveryReport;
            }
            catch(Exception e)
            {
                logger.Error(e);
                throw;
            }
        }

        public async Task<DeliveryReport<string, string>> Publish(string topic, int partition, string payload)
        {
            try
            {
                var message = new Message<string, string>();
                message.Key = partition.ToString();
                message.Value = payload;
                var deliveryReport = await producer.ProduceAsync(topic, message);
                return deliveryReport;
            }
            catch (Exception e)
            {
                logger.Error(e);
                throw;
            }
        }
    }
}
