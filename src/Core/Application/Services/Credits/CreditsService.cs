using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Core.Application.Data.AutoMapperConfiguration;
using Core.Application.Data.Criteria;
using Core.Application.Data.QueryExtensions.Credits.Cast;
using Core.Application.Data.QueryExtensions.Credits.Crew;
using Core.Application.Dto;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Services.Credits
{
    public class CreditsService : ICreditsService
    {
        private readonly IMovieContext _db;  

        public CreditsService(IMovieContext db)
        {
            _db = db;
        }
            
        public async Task<List<ActorDto>> GetTopCastAsync(int creditId, int count)
        {
            return await _db.Cast
                .GetTopCast(creditId, count)
                .ProjectTo<ActorDto>(AutoMapperConfiguration.Config)
                .ToListAsync();
        }   

        public async Task<List<EmployeeDto>> GetCrewAsync(int creditId, CrewCriteria filters)
        {
            return await _db.Crew
                .Where(c => c.CreditId == creditId)
                .FilterByCriteria(filters)
                .ProjectTo<EmployeeDto>(AutoMapperConfiguration.Config)
                .ToListAsync();
        }

        public async Task<List<FilmographyItemDto>> GetPersonFilmographyAsync(int personId)
        {
            var cast = _db.Cast
                .Where(c => c.ActorId == personId)
                .ProjectTo<JobDto>(AutoMapperConfiguration.Config);

            var crew = _db.Crew
                .Where(c => c.PersonId == personId)
                .ProjectTo<JobDto>(AutoMapperConfiguration.Config);

            var filmography = await cast
                .Union(crew)
                .OrderBy(c => c.ReleaseDate)
                .ToListAsync();

            return filmography
                .GroupBy(c => new { c.MovieId, c.ReleaseDate, c.Title, c.Department })
                .Select(g => new FilmographyItemDto 
                {
                    MovieId = g.Key.MovieId,
                    Title = g.Key.Title,
                    Department = g.Key.Department,
                    ReleaseDate = g.Key.ReleaseDate,
                    PersonPositions = g.Select(i => i.Position).ToList()
                }).ToList();
        }
    }
}
