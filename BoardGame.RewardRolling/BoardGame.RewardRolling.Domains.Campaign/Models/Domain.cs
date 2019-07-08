using BoardGame.RewardRolling.Domains.Campaign.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Domains.Campaign.Models
{
    public abstract class Domain
    {
        public List<DomainEvent> Events { get; set; }
        public bool Created { get; set; }
        public bool Modified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        protected virtual void Create()
        {
            Created = true;
            CreatedAt = DateTime.UtcNow;
        }
        protected virtual void Modify()
        {
            Modified = true;
            ModifiedAt = DateTime.Now;
        }
        protected void AddEvent(DomainEvent domainEvent)
        {
            Events = Events ?? new List<DomainEvent>();
            Events.Add(domainEvent);
        }
    }
}
