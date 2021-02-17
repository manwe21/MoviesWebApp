using System.Threading.Tasks;
using Core.Application.Exceptions;
using Core.Application.Exceptions.HttpExceptions;
using Core.Application.Services.Folders;
using Core.Application.Services.Votes;
using Xunit;

namespace IntegrationTests.Services.Votes
{
    
    public class VotesServiceTests : TestBase
    {
        [Fact]
        public async Task RateMovie_VoteAdded()
        {
            var db = CreateAndSeedDb();
            var folderService = new FolderService(db);
            var service = new VoteService(db, folderService);

            await service.RateMovieAsync(UserId1, 4, 7);

            Assert.Contains(db.Votes,
                vote => vote.UserId == UserId1
                        && vote.MovieId == 4
                        && vote.Value.HasValue
                        && vote.Value.Value == 7);
        }
        
        [Fact]
        public async Task RateMovie_MovieRatingChanged()
        {
            var db = CreateAndSeedDb();
            var folderService = new FolderService(db);
            var service = new VoteService(db, folderService);

            await service.RateMovieAsync(UserId1, 4, 7);

            var movie = await db.Movies.FindAsync(4);
            Assert.NotStrictEqual(7.99, movie.Rating);
            Assert.Equal(101, movie.VotesCount);
        }
        
        [Fact]
        public async Task RateMovie_MovieDoesNotExist_ThrownException()
        {
            var db = CreateAndSeedDb();
            var folderService = new FolderService(db);
            var service = new VoteService(db, folderService);

            await Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
            {
                await service.RateMovieAsync(UserId1, 7, 7);
            });
        }

        [Fact]
        public async Task MarkMovieAsWatched_VoteAdded()
        {
            var db = CreateAndSeedDb();
            var folderService = new FolderService(db);
            var service = new VoteService(db, folderService);

            await service.MarkMovieAsWatchedAsync(UserId1, 4);
            
            Assert.Contains(db.Votes,
                vote => vote.UserId == UserId1
                        && vote.MovieId == 4
                        && !vote.Value.HasValue);
        }

        [Fact]
        public async Task UnmarkMovieAsWatched_VoteRemoved()
        {
            var db = CreateAndSeedDb();
            var folderService = new FolderService(db);
            var service = new VoteService(db, folderService);

            await service.UnmarkMovieAsWatchedAsync(UserId1, 1);
            
            Assert.DoesNotContain(db.Votes, vote => vote.UserId == UserId1 && vote.MovieId == 1);
        }
        
        [Fact]
        public async Task UnmarkMovieAsWatched_MovieIsNotInWatchedList_ThrownException()
        {
            var db = CreateAndSeedDb();
            var folderService = new FolderService(db);
            var service = new VoteService(db, folderService);

            await Assert.ThrowsAsync<BadRequestException>(async () =>
            {
                await service.UnmarkMovieAsWatchedAsync(UserId1, 4);
            });
        }
    
        [Fact]
        public async Task GetVotesSimilarity_CorrectValueReturned()
        {
            var db = CreateAndSeedDb();
            var folderService = new FolderService(db);
            var service = new VoteService(db, folderService);
            
            int res = await service.GetVotesSimilarityAsync(UserId1, UserId2, 2);
            
            Assert.Equal(26, res);
        }
        
        [Fact]
        public async Task GetVotesSimilarity_NotEnoughMutualVotes_ExceptionThrown()
        {
            var db = CreateAndSeedDb();
            var folderService = new FolderService(db);
            var service = new VoteService(db, folderService);

            await Assert.ThrowsAsync<NotEnoughVotesException>(async () =>
            {
                await service.GetVotesSimilarityAsync("", "", 5);
            });
        }
        
    }
}
