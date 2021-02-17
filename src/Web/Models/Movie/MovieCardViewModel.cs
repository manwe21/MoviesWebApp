using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Movie
{
    public class MovieCardViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Rating { get; set; }
        public string PosterPath { get; set; }
    }
}
