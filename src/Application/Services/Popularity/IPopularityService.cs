using System.Threading.Tasks;
using Application.Data.QueryExtensions.Pagination;
using Application.Dto;

namespace Application.Services.Popularity
{
    public interface IPopularityService
    {
        Task<PagedResult<MovieDto>> ListPopularMoviesAsync(int pageSize, int pageNumber);    
    }
}
    