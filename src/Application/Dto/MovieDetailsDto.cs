using System;
using System.Collections.Generic;

namespace Application.Dto
{
    public class MovieDetailsDto
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PosterPath { get; set; }
        public double Rating { get; set; }
        public int VotesCount { get; set; }
        public int CreditId { get; set; }
        public List<GenreDto> Genres { get; set; }
        public List<EmployeeDto> Directors { get; set; }

    }
}
