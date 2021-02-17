using System.Linq;
using Core.Domain.Entities;

namespace Core.Application.Data
{
    public interface ISearchDataService 
    {
        IQueryable<Movie> SearchMovies(string query); 
        IQueryable<Person> SearchPeople(string query);
    }
}
