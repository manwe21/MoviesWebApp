using System.Collections.Generic;
using System.Linq;
using Application.Data.QueryExtensions.Folders;
using Infrastructure.Data;
using Xunit;

namespace IntegrationTests.DataQueries
{
    public class FoldersQueriesTests : TestBase
    {
        private readonly MovieContext _db;

        public FoldersQueriesTests()
        {
            _db = CreateAndSeedDb();
        }

        [Fact]
        public void GetFoldersWithMovie_CorrectQueryReturned()
        {
            var folders = _db.Folders.GetFoldersWithMovie(UserId1, 1);

            var correctIds = new List<int> { 1, 3 };
            Assert.Equal(correctIds, folders.Select(f => f.Id));
        }

        [Fact]
        public void IsFolderWithNameAlreadyExists_FolderExists_TrueReturned()
        {
            var exists = _db.Folders.IsFolderWithNameAlreadyExists(UserId1, "Folder1");
            
            Assert.True(exists);
        }
        
        [Fact]
        public void IsFolderWithNameAlreadyExists_FolderNotExists_FalseReturned()
        {
            var exists = _db.Folders.IsFolderWithNameAlreadyExists(UserId1, "Folder2");
            
            Assert.False(exists);
        }
        
    }
}
