using System;
using System.Linq;
using Application.Data.Criteria;
using Domain.Entities;

namespace Application.Data.QueryExtensions.Credits.Crew
{
    public static class CrewFilters 
    {
        public static IQueryable<Employee> FilterByCriteria(this IQueryable<Employee> source, CrewCriteria criteria)
        {
            return source
                .FilterByJob(criteria.Job)
                .FilterByDepartment(criteria.Department);
        }
        
        public static IQueryable<Employee> FilterByJob(this IQueryable<Employee> source, string job)
        {
            if (String.IsNullOrEmpty(job))
                return source;
            return source.Where(e => e.Job == job);
        }

        public static IQueryable<Employee> FilterByDepartment(this IQueryable<Employee> source, string department)
        {
            if (String.IsNullOrEmpty(department))
                return source;
            return source.Where(e => e.Job == department);
        }
    }
}
