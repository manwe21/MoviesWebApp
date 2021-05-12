using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Data.AutoMapperConfiguration;
using Application.Dto;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Genre
{
    public class GenreService : IGenresService
    {
        private readonly IMovieContext _db;

        public GenreService(IMovieContext db)
        {
            _db = db;
        }

        public async Task<List<GenreDto>> ListGenresAsync()
        {
            return await _db.Genres
                .ProjectTo<GenreDto>(AutoMapperConfiguration.Config)
                .ToListAsync();
        }
    }
}
