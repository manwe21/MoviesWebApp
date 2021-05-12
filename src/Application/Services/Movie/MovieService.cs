using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Data.AutoMapperConfiguration;
using Application.Data.Criteria;
using Application.Data.QueryExtensions.Common;
using Application.Data.QueryExtensions.Movies;
using Application.Data.QueryExtensions.Pagination;
using Application.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Movie
{
    public class MovieService : IMovieService
    {
        private readonly IMovieContext _db;
        private readonly IMapper _mapper;

        public MovieService(IMovieContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }
            
        public async Task<PagedResult<MovieDto>> ListMoviesAsync(MovieCriteria filters, string orderBy, int pageSize, int pageNumber)
        {
            var movies = await _db.Movies
                .FilterByCriteria(filters)
                .OrderByPropertyName(orderBy)
                .ThenByDescending(m => m.VotesCount)
                .ProjectTo<MovieDto>(AutoMapperConfiguration.Config)
                .AsNoTracking()
                .PaginateAsync(pageNumber, pageSize);
            return movies;
        }

        public async Task<MovieDto> GetMovieAsync(int id)
        {
            var movie = await _db.Movies.FindAsync(id);
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<List<MovieDto>> GetUpcomingMoviesAsync(int count)
        {
            var movies = await _db.Movies
                //.GetUpcomingMovies()
                .ProjectTo<MovieDto>(AutoMapperConfiguration.Config)
                .Take(count)
                .ToListAsync();
            return movies;
        }

        public async Task<MovieDetailsDto> GetMovieDetailsAsync(int id)
        {
            var movie = await _db.Movies
                .Where(m => m.Id == id)
                .ProjectTo<MovieDetailsDto>(AutoMapperConfiguration.Config)
                .FirstOrDefaultAsync();
            return movie;
        }
            
        public async Task<List<MovieDto>> GetFamousMoviesForPersonAsync(int personId, int count)
        {
            return await _db.Movies
                .GetFamousMoviesForPerson(personId)
                .Take(count)
                .ProjectTo<MovieDto>(AutoMapperConfiguration.Config)
                .ToListAsync();
        }

    }
}
