using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Domain.Entities
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }   
        public string PosterPath { get; set; }
        public double Rating { get; set; }
        public int VotesCount { get; set; }
        public int CreditId { get; set; }
        public Credit Credit { get; set; }
    

        public List<MovieGenre> MovieGenres { get; set; }
        public List<MovieFolder> MovieFolders { get; set; }
        public List<Vote> Votes { get; set; }

        public Movie()
        {
            MovieGenres = new List<MovieGenre>();
            MovieFolders = new List<MovieFolder>();
            Votes = new List<Vote>();
        }

        public void AddVote(int value)    
        {
            VotesCount++;
            Rating += (value - Rating) / VotesCount;
        }

        public void ChangeVote(int oldValue, int newValue)
        {
            var oldRating = (Rating * VotesCount - oldValue) / (VotesCount - 1);   //Rating without user`s value
            Rating = oldRating + (newValue - oldRating) / VotesCount;
        }

        public void DeleteVote(int value)
        {
            Rating = (Rating * VotesCount - value) / (VotesCount - 1);
            VotesCount--;
        }

    }
}
