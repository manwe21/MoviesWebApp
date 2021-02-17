using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Core.Application.Data;
using Core.Application.Data.AutoMapperConfiguration;
using Core.Application.Data.QueryExtensions.Pagination;
using Core.Application.Dto;
using Microsoft.Extensions.Logging;

namespace Core.Application.Services.Search
{
    public class SearchService : ISearchService
    {
        private readonly ISearchDataService _searchDataService;

        public SearchService(ISearchDataService searchDataService)
        {
            _searchDataService = searchDataService;
        }

        public async Task<PagedResult<MovieSearchItemDto>> SearchMoviesAsync(string searchString, int page, int pageSize)
        {
            var movies = _searchDataService.SearchMovies(searchString);
            return await movies
                .ProjectTo<MovieSearchItemDto>(AutoMapperConfiguration.Config)
                .PaginateAsync(page, pageSize);
        }

        public async Task<PagedResult<PersonDto>> SearchPeopleAsync(string searchString, int page, int pageSize)
        {
            var people = _searchDataService.SearchPeople(searchString);
            return await people
                .ProjectTo<PersonDto>(AutoMapperConfiguration.Config)
                .PaginateAsync(page, pageSize);
        }
    }
}
