using System.Collections.Generic;
using System.Linq;
using Application.Data.QueryExtensions.Credits.Cast;
using Xunit;

namespace IntegrationTests.DataQueries
{
    public class CreditsQueriesTests : TestBase
    {
        [Fact]
        public void GetTopCast_CorrectQueryReturned()
        {
            var db = CreateAndSeedDb();
            var topCast = db.Cast.GetTopCast(2).ToList();

            var correctIds = new List<int> { 3, 5, 4, 6 };
            Assert.Equal(correctIds, topCast.Select(r => r.Id));
        }
    }
}
