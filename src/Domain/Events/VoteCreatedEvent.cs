using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class VoteCreatedEvent : DomainEvent
    {
        public Vote Vote { get; }
        
        public VoteCreatedEvent(Vote vote)
        {
            Vote = vote;
        }
    }
}
