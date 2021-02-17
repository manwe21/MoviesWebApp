using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Dto
{
    public class MovieWithVoteDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PosterPath { get; set; }
        public double Rating { get; set; }
        public int VotesCount { get; set; }
        public VoteDto UserVote { get; set; }
    }
}
