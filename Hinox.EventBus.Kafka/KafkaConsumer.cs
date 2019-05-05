using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;

namespace NetCore.EventBus.Kafka
{
    public class KafkaConsumer : Consumer<string, string>
    {
        public KafkaConsumer(KafkaConfig config) : base(config.GetConsumerConfig())
        {

        }
        public KafkaConsumer(Dictionary<string,string> config) : base(config)
        {

        }
    }
}
