using System.Collections.Generic;

namespace Core.Domain.Common
{
    public abstract class BaseEntity
    {
        public List<DomainEvent> Events { get; } = new List<DomainEvent>();
    }
}
