using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace NetCore.Queue.Kafka
{
    public abstract class BaseKafkaMessageProcessor
    {
        public abstract void ProcessError(Error error);
        public abstract Task ProcessMessageAsync(ConsumeResult<string, string> message);
    }
}
