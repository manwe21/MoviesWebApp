using System;
using System.Collections.Generic;

namespace Web.Models.Person
{
    public class FilmographyItemViewModel
    {
        public string Title { get; set; }
        public int MovieId { get; set; }
        public List<string> PersonPositions { get; set; }
    }
}