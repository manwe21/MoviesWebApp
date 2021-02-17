using System;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Application.Data.QueryExtensions.Common   
{
    public static class OrderByPropertyNameExtension   
    {
        public static IOrderedQueryable<T> OrderByPropertyName<T>(this IQueryable<T> source, string propName)
        {
            var orderingParams = propName.Split(".");
            var orderProperty = orderingParams[0];
            var orderDirection = orderingParams[1];
                
            var property = typeof(T).GetProperty(orderProperty);
            if (property == null)
                throw new ArgumentException("Ordering property is invalid");

            var parameter = Expression.Parameter(typeof(T), "x");
            var selectorExpr = Expression.Lambda(Expression.Property(parameter, property), parameter);

            Expression queryExpr = source.Expression;
            queryExpr = Expression.Call(typeof(Queryable), orderDirection == "asc" ? "OrderBy" : "OrderByDescending",
                new []
                {
                    source.ElementType,
                    property.PropertyType
                }, queryExpr, selectorExpr);
            
            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(queryExpr);
        }
    }
}
