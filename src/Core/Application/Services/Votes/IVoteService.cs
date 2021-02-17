using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Application.Data.QueryExtensions.Pagination;
using Core.Application.Dto;

namespace Core.Application.Services.Votes
{       
    public interface IVoteService
    {
        Task RateMovieAsync(string userId, int movieId, int value);
        Task MarkMovieAsWatchedAsync(string userId, int movieId);  
        Task UnmarkMovieAsWatchedAsync(string userId, int movieId);
        Task<int> GetVotesSimilarityAsync(string ownerId, string userId, int minMutualVotes);
        Task<VoteDto> GetVoteAsync(string userId, int movieId); 
    }
}       
