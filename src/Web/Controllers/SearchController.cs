using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dto;
using Application.Services.Search;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models.Search;

namespace Web.Controllers
{
    public class SearchController : BaseController
    {
        private readonly ISearchService _searchService;
        private ILogger<SearchController> _logger;
        private readonly IMapper _mapper;

        private const int TopSearchResultsCount = 3;

        public SearchController(ISearchService searchService, ILogger<SearchController> logger, IMapper mapper)
        {
            _searchService = searchService; 
            _logger = logger;
            _mapper = mapper;
        }
        
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetSearchDropdown(string query)
        {
            var movies = await _searchService.SearchMoviesAsync(query, 1, TopSearchResultsCount);
            var people = await _searchService.SearchPeopleAsync(query, 1, TopSearchResultsCount);

            var moviesResult = _mapper.Map<List<MovieSearchItemDto>, List<SearchItemViewModel>>(movies.Results);
            var peopleResult = _mapper.Map<List<PersonDto>, List<SearchItemViewModel>>(people.Results);
            var result = moviesResult.Union(peopleResult).ToList();

            var model = new SearchDropdownViewModel
            {
                Query = query,
                SearchItems = result
            };
            return PartialView("SearchDropdownMenu", model);
        }
            
    }
}
