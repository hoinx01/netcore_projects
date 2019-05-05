using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;

namespace NetCore.Queue.Kafka
{
    public class KafkaProducer : Producer<string, string>
    {
        public KafkaProducer(KafkaConfig kafkaConfig) : base(kafkaConfig.GetProducerConfig())
        {

        }
    }
}
