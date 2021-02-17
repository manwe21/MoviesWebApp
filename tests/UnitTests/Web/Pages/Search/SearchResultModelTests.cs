using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Pages.Search;
using Xunit;

namespace UnitTests.Web.Pages.Search
{
    public class SearchResultModelTests : TestBase
    {
        [Fact]
        public async Task GetAsync_CategoryIsNotExists_404Returned()
        {
            var model = new Result(null, Mapper);
            model.CategoryString = "CategoryThatDoesNotExist";

            var result = await model.OnGetAsync();

            Assert.IsType<NotFoundResult>(result);

        }
    }
}