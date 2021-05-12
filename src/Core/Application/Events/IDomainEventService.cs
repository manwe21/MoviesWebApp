using System.Threading.Tasks;
using Core.Domain.Common;

namespace Core.Application.Events
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}