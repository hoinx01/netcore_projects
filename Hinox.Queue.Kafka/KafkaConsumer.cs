using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using NetCore.Queue.Kafka;

namespace NetCore.Queue.Kafka
{
    public class KafkaConsumer : Consumer<string, string>
    {
        public KafkaConsumer(KafkaConfig config) : base(config.GetConsumerConfig())
        {

        }
    }
}
