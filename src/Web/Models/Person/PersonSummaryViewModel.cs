using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Movie;

namespace Web.Models.People
{
    public class PersonSummaryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }  
    }
}
