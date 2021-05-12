using System.Threading.Tasks;
using Application.Data;
using Application.Data.AutoMapperConfiguration;
using Application.Data.QueryExtensions.Pagination;
using Application.Dto;
using AutoMapper.QueryableExtensions;

namespace Application.Services.Search
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
