using System.Linq;
using Domain.Entities;

namespace Application.Data.QueryExtensions.Votes
{
    public static class VoteQueries
    {
        public static IQueryable<Vote> GetVotesForUser(this IQueryable<Vote> source, string userId)
        {
            return source
                .Where(v => v.UserId == userId)
                .Where(v => v.Value.HasValue)    
                .OrderBy(v => v.Movie);
        }

        public static IQueryable<Vote> GetMutualVotes(this IQueryable<Vote> source, string userId1, string userId2)
        {
            var votes1 = source.GetVotesForUser(userId1);
            var votes2 = source.GetVotesForUser(userId2);
            
            return votes1.Where(v => votes2.Select(v2 => v2.MovieId).Contains(v.MovieId));
        }

    }
}