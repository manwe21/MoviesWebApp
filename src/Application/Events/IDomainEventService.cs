using System.Threading.Tasks;
using Domain.Common;

namespace Application.Events
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}