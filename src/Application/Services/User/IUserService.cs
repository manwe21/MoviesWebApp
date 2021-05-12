using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Data.QueryExtensions.Pagination;
using Application.Dto;

namespace Application.Services.User
{
    public interface IUserService
    {   
        Task<List<UserGenreDto>> GetGenresAsync(string userId);
        Task<List<UserYearDto>> GetYearsAsync(string userId);
        Task<List<UserActorDto>> GetActorsAsync(string userId, int minMovies, int count);
        Task<PagedResult<MovieWithVoteDto>> ListMoviesAsync(string userId, int page, int pageSize);
        Task<PagedResult<MovieWithGuestVoteDto>> ListMoviesForCoupleAsync(string ownerId, string guestId, int page, int pageSize);
    }   
}
    