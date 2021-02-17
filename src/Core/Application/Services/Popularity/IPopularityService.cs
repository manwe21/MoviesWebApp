using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Application.Data.QueryExtensions.Pagination;
using Core.Application.Dto;

namespace Core.Application.Services.Popularity
{
    public interface IPopularityService
    {
        Task<PagedResult<MovieDto>> ListPopularMoviesAsync(int pageSize, int pageNumber);    
    }
}
    