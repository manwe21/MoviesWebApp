using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Person;

namespace Web.Models.Movie
{
    public class MovieCastViewModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        
        public List<ActorViewModel> Cast { get; set; }
        public List<ActorViewModel> Crew { get; set; }
    }
}
