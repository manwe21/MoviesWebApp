using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Data.Criteria;
using Application.Dto;

namespace Application.Services.Credits
{
    public interface ICreditsService         
    {
        Task<List<ActorDto>> GetTopCastAsync(int creditId, int count = Int32.MaxValue);     
        Task<List<EmployeeDto>> GetCrewAsync(int creditId, CrewCriteria filters);
        Task<List<FilmographyItemDto>> GetPersonFilmographyAsync(int personId);

    }
}
