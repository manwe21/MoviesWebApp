using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Data.QueryExtensions.Pagination;
using Core.Application.Services.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models.Search;
using Web.Models.SearchTable;

namespace Web.Pages.Search
{
    public class Result : PageModel
    {
        private readonly ISearchService _searchService;
        private readonly IMapper _mapper;
        
        private const int PageSize = 20;

        [BindProperty(Name = "category", SupportsGet = true)]
        public string CategoryString { get; set; }
        
        [BindProperty(Name = "query", SupportsGet = true)]
        public string Query { get; set; }
        
        public PagedResult<BaseRowViewModel> SearchResultItems { get; set; }
        
        public Result(ISearchService searchService, IMapper mapper)
        {
            _searchService = searchService;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync([FromQuery(Name = "page_n")]int pageNumber = 1)
        {
            var success = Enum.TryParse(CategoryString, true, out SearchCategory category);
            if (!success)
                return NotFound();
            
            SearchResultItems = await SearchForCategory(Query, category, pageNumber);
            return Page();
        }

        private async Task<PagedResult<BaseRowViewModel>> SearchForCategory(string query, SearchCategory searchCategory, int page)
        {
            PagedResult<BaseRowViewModel> result = null;
            switch (searchCategory)
            {
                case SearchCategory.Movies:
                    var movies = await _searchService.SearchMoviesAsync(query, page, PageSize);
                    result = _mapper.Map<PagedResult<BaseRowViewModel>>(movies);
                    break;
                
                case SearchCategory.People:
                    var people = await _searchService.SearchPeopleAsync(query, page, PageSize);
                    result = _mapper.Map<PagedResult<BaseRowViewModel>>(people);
                    
                    break;
            }
            return result;
        }
    }

}
