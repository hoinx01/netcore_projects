using NetCore.EventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.EventBus
{
    public class Subscriber
    {
        public string Group { get; set; }
        public IIntegrationEventHandler Handler { get; set; }
    }
}
