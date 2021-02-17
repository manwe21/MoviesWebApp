using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Core.Application.Data.QueryExtensions.Movies;
using Infrastructure.Data;
using Xunit;

namespace IntegrationTests.DataQueries
{
    public class MovieQueriesTests : TestBase
    {
        private readonly MovieContext _db;

        public MovieQueriesTests()
        {
            _db = CreateAndSeedDb();
        }

        [Fact]
        public void GetFamousMoviesForPerson_CorrectQueryReturned()
        {
            var movies = _db.Movies.GetFamousMoviesForPerson(1);

            var correctIds = new List<int> { 3, 1, 2 };
            Assert.Equal(correctIds, movies.Select(m => m.Id));
        }

        [Fact]
        public void GetMoviesFromFolder_CorrectQueryReturned()
        {
            var movies = _db.Movies.GetMoviesFromFolder(3);

            var correctIds = new List<int> { 1, 2, 3 };
            Assert.Equal(correctIds, movies.Select(m => m.Id));
        }

        [Fact]
        public void GetUserMovies_CorrectQueryReturned()
        {
            var movies = _db.Movies.GetUserMovies(UserId1);

            var correctIds = new List<int> { 1, 2, 3 };
            Assert.Equal(correctIds, movies.Select(m => m.Id));
        }

        [Fact]
        public void GetUpcomingMovies_CorrectQueryReturned()
        {
            var movies = _db.Movies.GetUpcomingMovies();

            var correctIds = new List<int> { 6, 5 };
            Assert.Equal(correctIds, movies.Select(m => m.Id));
        }
        
    }
}