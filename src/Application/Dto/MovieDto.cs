using System;

namespace Application.Dto
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }    
        public DateTime ReleaseDate { get; set; }
        public string PosterPath { get; set; }
        public double Rating { get; set; }
        public int VotesCount { get; set; }
        public int CreditId { get; set; }
    }
}
