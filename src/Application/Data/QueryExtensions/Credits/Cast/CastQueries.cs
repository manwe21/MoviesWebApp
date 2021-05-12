using System;
using System.Linq;
using Domain.Entities;

namespace Application.Data.QueryExtensions.Credits.Cast
{
    public static class CastQueries  
    {   
        public static IQueryable<Role> GetTopCast(this IQueryable<Role> source, int creditId, int count = Int32.MaxValue)
        {
            return source
                .Where(r => r.CreditId == creditId)
                .OrderBy(r => r.Order)
                .Take(count);
        }

    }
}
