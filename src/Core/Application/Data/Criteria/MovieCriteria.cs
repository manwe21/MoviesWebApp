using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Data.Criteria
{
    public class MovieCriteria  
    {
        public DateTime? ReleaseDateFrom { get; set; }
        public DateTime? ReleaseDateTo { get; set; }
        public int? RatingFrom { get; set; }
        public int? RatingTo { get; set; }  
        public int? VotesCountFrom { get; set; } = 300;
        public int? VotesCountTo { get; set; }
        public List<int> Genres { get; set; } = new List<int>();
    }
}
