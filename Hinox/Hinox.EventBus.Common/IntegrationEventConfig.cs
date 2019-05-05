using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.EventBus
{
    public class SubcriberDescription
    {
        public string HandlerType { get; set; }
        public string Group { get; set; }
        public int InstanceNumber { get; set; }
    }
    public class EventDescription
    {
        public string Type { get; set; }
        public List<SubcriberDescription> SubscriberDescriptions { get; set; }
    }
    public class IntegrationEventConfig
    {
        public List<EventDescription> EventDescriptions { get; set; }
    }
}
