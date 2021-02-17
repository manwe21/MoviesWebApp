using System.Threading.Tasks;
using Core.Application.Data.QueryExtensions.Pagination;
using Xunit;

namespace IntegrationTests.DataQueries
{
    public class PaginationTests : TestBase
    {
        [Fact]
        public async Task Paginate_CorrectQueryReturned()
        {
            var db = CreateAndSeedDb();

            var movies = await db.Movies.PaginateAsync(2, 2);
            
            Assert.Equal(2, movies.PageNumber);
            Assert.Equal(6, movies.AllRows);
            Assert.Equal(3, movies.PagesCount);
            Assert.Equal(2, movies.PageSize);
        }
    }
}