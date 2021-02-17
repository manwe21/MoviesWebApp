using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Data.QueryExtensions.Credits.Cast
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
