using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services.Folders;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Events.EventHandlers.VoteCreated
{
    public class RemoveMovieFromWatchListEventHandler : INotificationHandler<DomainEventNotification<VoteCreatedEvent>>
    {
        private readonly IFolderService _folderService;
        private readonly ILogger<RemoveMovieFromWatchListEventHandler> _logger;

        public RemoveMovieFromWatchListEventHandler(ILogger<RemoveMovieFromWatchListEventHandler> logger, IFolderService folderService)
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
