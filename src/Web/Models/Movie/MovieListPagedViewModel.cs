using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Movie
{
    public class MovieListPagedViewModel
    {
        public List<MovieCardViewModel> Movies { get; set; }
        public int RowsCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PagesCount { get; set; }
    }
}
