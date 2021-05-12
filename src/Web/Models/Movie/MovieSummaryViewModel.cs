using System;
using System.Collections.Generic;
using Application.Dto;

namespace Web.Models.Movie
{
    public class MovieSummaryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }    
        public string Overview { get; set; }
        public List<string> Genres { get; set; }
        public DateTime ReleaseDate { get; set; }  
        public string PosterPath { get; set; }
        public double Rating { get; set; }  
        public int VotesCount { get; set; }
        public List<EmployeeDto> Directors { get; set; }
    }
}
