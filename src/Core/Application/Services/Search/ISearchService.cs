using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.Data.QueryExtensions.Pagination;
using Core.Application.Dto;

namespace Core.Application.Services.Search
{
    public interface ISearchService
    {
        Task<PagedResult<MovieSearchItemDto>> SearchMoviesAsync(string searchString, int page, int pageSize);
        Task<PagedResult<PersonDto>> SearchPeopleAsync(string searchString, int page, int pageSize);
    }
}   
        