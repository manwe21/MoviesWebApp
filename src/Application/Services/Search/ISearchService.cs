using System.Threading.Tasks;
using Application.Data.QueryExtensions.Pagination;
using Application.Dto;

namespace Application.Services.Search
{
    public interface ISearchService
    {
        Task<PagedResult<MovieSearchItemDto>> SearchMoviesAsync(string searchString, int page, int pageSize);
        Task<PagedResult<PersonDto>> SearchPeopleAsync(string searchString, int page, int pageSize);
    }
}   
        