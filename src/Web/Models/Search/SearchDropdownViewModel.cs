using System.Collections.Generic;

namespace Web.Models.Search
{
    public class SearchDropdownViewModel
    {
        public string Query { get; set; }
        public List<SearchItemViewModel> SearchItems { get; set; }
    }
}