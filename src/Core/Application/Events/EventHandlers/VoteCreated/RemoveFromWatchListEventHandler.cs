using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Services.Folders;
using Core.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Events.EventHandlers.VoteCreated
{
    public class RemoveFromWatchListEventHandler : INotificationHandler<DomainEventNotification<VoteCreatedEvent>>
    {
        private readonly IFolderService _folderService;
        private readonly ILogger<RemoveFromWatchListEventHandler> _logger;

        public RemoveFromWatchListEventHandler(ILogger<RemoveFromWatchListEventHandler> logger, IFolderService folderService)
        {
            _logger = logger;
            _folderService = folderService;
        }

        public async Task Handle(DomainEventNotification<VoteCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var vote = notification.DomainEvent.Vote;
            var foldersWithMovie = await _folderService.GetFoldersWithMovieAsync(vote.UserId, vote.MovieId);
            var watchLater = foldersWithMovie.FirstOrDefault(f => f.Name == "Watch Later");
            if (watchLater != null)
            {
                await _folderService.DeleteMovieFromFolderAsync(vote.UserId, watchLater.Id, vote.MovieId);
            }
        }
    }
}
