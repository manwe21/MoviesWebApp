using System.Collections.Generic;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public List<DomainEvent> Events { get; } = new List<DomainEvent>();
    }
}
