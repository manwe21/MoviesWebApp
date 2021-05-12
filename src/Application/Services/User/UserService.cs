using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Data.AutoMapperConfiguration;
using Application.Data.QueryExtensions.Movies;
using Application.Data.QueryExtensions.Pagination;
using Application.Dto;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services.User
{
    public class UserService : IUserService
    {
        private IMovieContext _db;
        private ILogger<UserService> _logger;

        public UserService(IMovieContext db, ILogger<UserService> logger)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<List<UserGenreDto>> GetGenresAsync(string userId)
        {
            var genres = await _db.Votes
                .Where(v => v.UserId == userId && v.Value.HasValue)
                .SelectMany(v => v.Movie.MovieGenres.Select(mg => new { Name = mg.Genre.Name, Rating = v.Value.Value }))
                .GroupBy(g => g.Name)
                .Select(group => new UserGenreDto
                {
                    Name = group.Key,
                    Count = group.Count(),
                    AverageRating = group.Average(g => g.Rating)
                })
                .ToListAsync();

            return genres;
        }

        public async Task<List<UserYearDto>> GetYearsAsync(string userId)
        {
            var years = await _db.Votes
                .Where(v => v.UserId == userId)
                .Select(v => v.Movie.ReleaseDate.Year)
                .GroupBy(y => y)
                .Select(group => new UserYearDto {Year = group.Key, Count = group.Count()})
                .ToListAsync();

            return years;
        }

        public async Task<List<UserActorDto>> GetActorsAsync(string userId, int minMovies, int count)
        {
            var actors = await _db.Votes
                .Where(v => v.UserId == userId && v.Value.HasValue)
                .SelectMany(v => v.Movie.Credit.Cast.Select(c => new
                {
                    Id = c.ActorId,
                    Name = c.Actor.Name,
                    MovieId = v.Movie.Id,
                    Rating = v.Value.Value, 
                    ImagePath = c.Actor.ImagePath
                }))
                .Distinct()
                .GroupBy(o => new {o.Name, o.Id, o.ImagePath}).Select(group => new UserActorDto
                {
                    Id = group.Key.Id,
                    Name = group.Key.Name,
                    ImagePath = group.Key.ImagePath,
                    AverageRating = group.Average(g => g.Rating),
                    MoviesCount = group.Count()
                })
                .Where(o => o.MoviesCount >= minMovies)
                .OrderByDescending(o => o.MoviesCount)
                .Take(count)
                .ToListAsync();

            return actors;
        }

        public async Task<PagedResult<MovieWithVoteDto>> ListMoviesAsync(string userId, int page, int pageSize)
        {
            var movies = await _db.Movies
                .GetUserMovies(userId)
                .ProjectTo<MovieWithVoteDto>(AutoMapperConfiguration.Config, new { userId })
                .PaginateAsync(page, pageSize);
            return movies;
        }

        public async Task<PagedResult<MovieWithGuestVoteDto>> ListMoviesForCoupleAsync(string ownerId, string guestId, int page, int pageSize)
        {
            var movies = await _db.Movies
                .GetUserMovies(ownerId)
                .ProjectTo<MovieWithGuestVoteDto>(AutoMapperConfiguration.Config, new {userId = guestId, ownerId})
                .PaginateAsync(page, pageSize);
            return movies;
        }   
    }
}
