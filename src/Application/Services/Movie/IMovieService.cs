using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Data.Criteria;
using Application.Data.QueryExtensions.Pagination;
using Application.Dto;

namespace Application.Services.Movie
{           
    public interface IMovieService  
    {
        Task<PagedResult<MovieDto>> ListMoviesAsync(MovieCriteria filters, string orderBy, int pageSize, int pageNumber);
        Task<MovieDto> GetMovieAsync(int id);
        Task<List<MovieDto>> GetUpcomingMoviesAsync(int count); 
        Task<MovieDetailsDto> GetMovieDetailsAsync(int id);   
        Task<List<MovieDto>> GetFamousMoviesForPersonAsync(int personId, int count);
    }   
}   
