using System.Collections.Generic;
using System.Linq;
using Application.Data.QueryExtensions.Votes;
using Infrastructure.Data;
using Xunit;

namespace IntegrationTests.DataQueries
{
    public class VotesQueriesTests : TestBase
    {
        private readonly MovieContext _db;

        public VotesQueriesTests()
        {
            _db = CreateAndSeedDb();
        }

        [Fact]
        public void GetVotesForUser_CorrectQueryReturned()
        {
            var votes = _db.Votes.GetVotesForUser(UserId1).ToList();

            Assert.Equal(1, votes[0].MovieId);
            Assert.Equal(2, votes[1].MovieId);
            Assert.Equal(3, votes[2].MovieId);
            Assert.Equal(5, votes[0].Value);
            Assert.Equal(10, votes[1].Value);
            Assert.Equal(7, votes[2].Value);
        }

        [Fact]
        public void GetMutualVotes_CorrectQueryReturned()
        {
            var votes = _db.Votes.GetMutualVotes(UserId1, UserId2).ToList();
            
            Assert.Equal(1, votes[0].MovieId);
            Assert.Equal(2, votes[1].MovieId);
            Assert.Equal(3, votes[2].MovieId);
            Assert.Equal(5, votes[0].Value);
            Assert.Equal(10, votes[1].Value);
            Assert.Equal(7, votes[2].Value);
        }

    }
}
