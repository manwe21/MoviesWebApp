using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Core.Application.Data.AutoMapperConfiguration;
using Core.Application.Dto;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Services.Genre
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
