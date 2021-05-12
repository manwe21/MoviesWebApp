using System.Linq;
using Domain.Entities;

namespace Application.Data
{
    public interface ISearchDataService 
    {
        IQueryable<Movie> SearchMovies(string query); 
        IQueryable<Person> SearchPeople(string query);
    }
}
