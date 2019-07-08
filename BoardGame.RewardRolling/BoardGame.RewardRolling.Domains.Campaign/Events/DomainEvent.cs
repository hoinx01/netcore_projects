using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Domains.Campaign.Events
{
    public class DomainEvent
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public DateTime CreatedAt { get; set; }

        public DomainEvent()
        {
            CreatedAt = DateTime.UtcNow;
            Type = this.GetType().Name;
            Id = Guid.NewGuid().ToString();
        }
    }
}
