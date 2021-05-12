using System.Threading.Tasks;
using Application.Data.QueryExtensions.Pagination;
using Domain.Entities;

namespace Application.Data.Repositories
{   
    public interface IPopularityRepository
    {
        Task<PagedResult<Movie>> ListPopularMoviesAsync(int pageSize, int pageNumber);
    }
}
