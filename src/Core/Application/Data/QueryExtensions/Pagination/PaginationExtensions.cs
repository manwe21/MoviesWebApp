using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Data.QueryExtensions.Pagination
{
    public static class PaginationExtensions
    {   
        public static async Task<PagedResult<T>> PaginateAsync<T>(this IQueryable<T> source, int currentPage, int pageSize)
        {
            var result = new PagedResult<T>
            {
                AllRows = await source.CountAsync(),
                PageNumber = currentPage,
                PageSize = pageSize,
                Results = await source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync(),
            };
            result.PagesCount = (int)Math.Ceiling((double)result.AllRows / result.PageSize);
            return result;
        }

        public static PagedResult<T> Paginate<T>(this IQueryable<T> source, int currentPage, int pageSize)
        {
            var result = new PagedResult<T>
            {
                AllRows = source.Count(),
                PageNumber = currentPage,
                PageSize = pageSize,
                Results = source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(),
            };
            result.PagesCount = (int)Math.Ceiling((double)result.AllRows / result.PageSize);
            return result;
        }
    }
}
