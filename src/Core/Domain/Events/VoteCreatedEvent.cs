using Core.Domain.Common;
using Core.Domain.Entities;

namespace Core.Domain.Events
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
