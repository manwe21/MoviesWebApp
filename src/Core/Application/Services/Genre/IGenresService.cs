using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.Dto;

namespace Core.Application.Services.Genre
{
    public interface IGenresService
    {
        Task<List<GenreDto>> ListGenresAsync();
    }
}
