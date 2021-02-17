using System.Collections.Generic;

namespace Web.Models.SearchTable
{
    public class MovieRowViewModel : BaseRowViewModel
    {
        public double Rating { get; set; }
        public int Year { get; set; }
        public List<string> Genres { get; set; }
    }
}
