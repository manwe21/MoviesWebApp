using Core.Domain.Entities;
using Xunit;

namespace UnitTests.Core.Domain
{
    public class MovieTests
    {
        private static Movie GetMovie()
        {
            return new Movie
            {
                Rating = 8.5,
                VotesCount = 30
            };
        }
        
        [Fact]
        public void AddVote_RatingChanged()
        {
            var movie = GetMovie();
            movie.AddVote(10);
            
            Assert.Equal(31, movie.VotesCount);
            Assert.NotStrictEqual(8.55, movie.Rating);
        }

        [Fact]
        public void ChangeVote_RatingChanged()
        {
            var movie = GetMovie();
            movie.ChangeVote(5, 10);
            Assert.Equal(30, movie.VotesCount);
            Assert.NotStrictEqual(8.67, movie.Rating);
        }

        [Fact]
        public void DeleteVote_RatingChanged()
        {
            var movie = GetMovie();
            movie.DeleteVote(5);
            
            Assert.Equal(29, movie.VotesCount);
            Assert.NotStrictEqual(8.62, movie.Rating);
        }
    }
}
