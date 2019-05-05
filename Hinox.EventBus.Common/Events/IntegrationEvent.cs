using System;

namespace NetCore.EventBus.Events
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
        public IntegrationEvent(string key) : this()
        {
            Key = key;
        }

        public Guid Id  { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Key { get; set; }
    }
}
