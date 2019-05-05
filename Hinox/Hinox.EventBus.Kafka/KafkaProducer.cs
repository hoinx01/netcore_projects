using Confluent.Kafka;
using System;

namespace NetCore.EventBus.Kafka
{
    public class KafkaProducer : Producer<string, string>
    {
        public KafkaProducer(KafkaConfig kafkaConfig) : base(kafkaConfig.GetProducerConfig())
        {

        }
    }
}
