using System.Threading.Tasks;
using Core.Application.Data.QueryExtensions.Pagination;
using Core.Domain.Entities;

namespace Core.Application.Data.Repositories
{   
    public interface IPopularityRepository
    {
        Task<PagedResult<Movie>> ListPopularMoviesAsync(int pageSize, int pageNumber);
    }
}
