using System.Linq;
using Core.Application;
using Core.Application.Data;
using Core.Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class SearchDataService : ISearchDataService
    {
        private readonly IMovieContext _db;
        private readonly ILogger<SearchDataService> _logger;

        public SearchDataService(IMovieContext db, ILogger<SearchDataService> logger)
        {
            _db = db;
            _logger = logger;
        }   

        public IQueryable<Movie> SearchMovies(string query)
        {
            string expression = CreateSearchExpression(query);
            var res = _db.SearchItems
                .FromSqlInterpolated($"SELECT * FROM containstable(Movies, Title, {expression})")
                .Join(_db.Movies, item => item.Key, movie => movie.Id, (item, movie) => new
                {
                    movie.Id,
                    item.Rank,
                    movie.Title,
                    movie.ReleaseDate,
                    movie.Rating,
                    movie.VotesCount,
                    movie.PosterPath,
                    movie.MovieGenres
                })
                .OrderByDescending(m => m.VotesCount * m.Rank)
                .Select(movie => new Movie
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    Rating = movie.Rating,
                    VotesCount = movie.VotesCount,
                    PosterPath = movie.PosterPath,
                    MovieGenres = movie.MovieGenres
                });
            return res;
        }

        public IQueryable<Person> SearchPeople(string query)
        {
            string expression = CreateSearchExpression(query);
            var res = _db.SearchItems
                .FromSqlInterpolated($"SELECT * FROM containstable(People, Name, {expression})")
                .Join(_db.People, item => item.Key, person => person.Id, (item, person) => new
                {
                    person.Id,
                    person.Name,
                    person.ImagePath,
                    item.Rank
                })
                .OrderByDescending(p => p.Rank)
                .Select(p => new Person
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImagePath = p.ImagePath
                });
            _logger.LogCritical(res.ToQueryString());
            return res;
        }

        private string CreateSearchExpression(string query)
        {
            var keyWords = query.Trim().Split("+");
            var res = "";
            for (var i = 0; i < keyWords.Length; i++)
            {
                var word = keyWords[i];
                res += $"(FORMSOF(INFLECTIONAL, \"{word}\") OR \"{word}*\")";
                if (i < keyWords.Length - 1)
                {
                    res += " and ";
                }
            }
            return res;
        }
    }
}
