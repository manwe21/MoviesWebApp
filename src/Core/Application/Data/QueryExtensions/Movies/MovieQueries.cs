using System;
using System.Linq;
using Core.Domain.Entities;

namespace Core.Application.Data.QueryExtensions.Movies
{
    public static class MovieQueries
    {
        public static IQueryable<Movie> GetFamousMoviesForPerson(this IQueryable<Movie> source, int personId)
        {
            var queryForCast = source
                .Where(m => m.Credit.Cast   
                .Select(c => c.ActorId)
                .Contains(personId));

            var queryForCrew = source
                .Where(m => m.Credit.Crew
                .Select(e => e.PersonId)
                .Contains(personId));

            return queryForCast
                .Union(queryForCrew)
                .Distinct()
                .OrderByDescending(m => m.VotesCount);
        }
        
        public static IQueryable<Movie> GetMoviesFromFolder(this IQueryable<Movie> source, int folderId)
        {
            return source.Where(movie => movie.MovieFolders.Any(mf => mf.FolderId == folderId));
        }

        public static IQueryable<Movie> GetUserMovies(this IQueryable<Movie> source, string userId)
        {
            return source.Where(m => m.Votes.Any(v => v.UserId == userId));
        }

        public static IQueryable<Movie> GetUpcomingMovies(this IQueryable<Movie> source)
        {
            return source
                .Where(m => m.ReleaseDate >= DateTime.Now)
                .OrderBy(m => m.ReleaseDate);
        }
    }
}
