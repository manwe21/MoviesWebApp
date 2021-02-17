using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.Dto;

namespace Web.Models.Movie
{
    public class MovieWithVoteViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PosterPath { get; set; }
        public double Rating { get; set; }
        public int VotesCount { get; set; }
        public DateTime AddingDate { get; set; }    
        public VoteDto GuestVote { get; set; }
        public VoteDto OwnerVote { get; set; }
    }
}
