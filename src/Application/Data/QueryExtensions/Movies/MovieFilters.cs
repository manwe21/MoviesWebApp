using System;
using System.Collections.Generic;
using System.Linq;
using Application.Data.Criteria;
using Domain.Entities;

namespace Application.Data.QueryExtensions.Movies    
{           
    public static class MovieFilters
    {
        public static IQueryable<Movie> FilterByCriteria(this IQueryable<Movie> source, MovieCriteria criteria)
        {
            return source
                .FilterByRating(criteria.RatingFrom, criteria.RatingTo)
                .FilterByGenres(criteria.Genres)
                .FilterByReleaseDate(criteria.ReleaseDateFrom, criteria.ReleaseDateTo)
                .FilterByVotesCount(criteria.VotesCountFrom, criteria.VotesCountTo);
        }
        
        public static IQueryable<Movie> FilterByRating(this IQueryable<Movie> source, double? higherThan, double? lowerThan)
        {
            return source.Where(m => (!higherThan.HasValue || m.Rating >= higherThan) &&
                                          (!lowerThan.HasValue || m.Rating <= lowerThan));
        }

        public static IQueryable<Movie> FilterByReleaseDate(this IQueryable<Movie> source, DateTime? dateFrom, DateTime? dateTo)
        {
            return source.Where(m => (!dateFrom.HasValue || m.ReleaseDate >= dateFrom) &&
                                          (!dateTo.HasValue || m.ReleaseDate <= dateTo));
        }

        public static IQueryable<Movie> FilterByVotesCount(this IQueryable<Movie> source, int? from, int? to)
        {
            return source.Where(m => (!from.HasValue || m.VotesCount >= from) &&
                                          (!to.HasValue || m.VotesCount <= to)); 
        }

        public static IQueryable<Movie> FilterByGenres(this IQueryable<Movie> source, List<int> genres)
        {
            foreach (var genreId in genres)
            {
                source = source.Where(m => m.MovieGenres.Select(mg => mg.Genre.Id).Contains(genreId));
            }

            return source;
        }
        
    }
}
