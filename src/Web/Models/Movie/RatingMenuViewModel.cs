using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Movie
{
    public class RatingMenuViewModel
    {
        public int MovieId { get; set; }
        public int MaxRating{ get; set; }
        public int UserRating{ get; set; }
    }
}   
        