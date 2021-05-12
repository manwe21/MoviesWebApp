using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dto;

namespace Application.Services.Genre
{
    public interface IGenresService
    {
        Task<List<GenreDto>> ListGenresAsync();
    }
}
