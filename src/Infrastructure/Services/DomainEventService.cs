using System;
using System.Threading.Tasks;
using Application.Events;
using Domain.Common;
using MediatR;

namespace Infrastructure.Services
{
    public class DomainEventService : IDomainEventService
    {
        private readonly IPublisher _publisher;

        public DomainEventService(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task Publish(DomainEvent domainEvent)
        {
            var notification = GetNotificationForEvent(domainEvent);
            await _publisher.Publish(notification);
            domainEvent.IsPublished = true;
        }

        private INotification GetNotificationForEvent(DomainEvent domainEvent)
        {
            return (INotification)Activator.CreateInstance(
                typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
        }
    }
}
