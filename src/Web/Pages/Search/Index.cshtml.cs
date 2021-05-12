using System.Linq;
using System.Threading.Tasks;
using Application.Data.QueryExtensions.Pagination;
using Application.Services.Search;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models.Search;
using Web.Models.SearchTable;

namespace Web.Pages.Search
{
    public class Index : PageModel
    {
        private readonly ISearchService _searchService;
        private readonly IMapper _mapper;
        
        private const int ResultsCount = 5;
        
        [BindProperty(Name = "query", SupportsGet = true)]
        public string Query { get; set; }
        
        public PagedResult<MovieRowViewModel> Movies { get; private set; }
        public PagedResult<PersonRowViewModel> People { get; private set; }
        
        public Index(ISearchService searchService, IMapper mapper)
        {
            _searchService = searchService;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var movies = await _searchService.SearchMoviesAsync(Query, 1, ResultsCount);
            var people = await _searchService.SearchPeopleAsync(Query, 1, ResultsCount);
            
            Movies = _mapper.Map<PagedResult<MovieRowViewModel>>(movies);
            People = _mapper.Map<PagedResult<PersonRowViewModel>>(people);
            
            return Page();
        }
    }
}