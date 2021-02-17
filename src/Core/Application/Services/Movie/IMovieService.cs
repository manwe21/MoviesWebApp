using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.Dto;
using Core.Application.Data.Criteria;
using Core.Application.Data.QueryExtensions.Pagination;

namespace Core.Application.Services.Movie
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
