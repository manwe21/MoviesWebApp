using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.Exceptions.HttpExceptions;
using Core.Application.Services.Folders;
using Xunit;

namespace IntegrationTests.Services.Folders
{
    public class FolderServiceTests : TestBase
    {
        [Fact]
        public async Task CreateDefaultFoldersForUser_FoldersCreated()
        {
            var db = CreateAndSeedDb();
            var service = new FolderService(db);

            var userId = Guid.NewGuid().ToString();
            await service.CreateDefaultFoldersForUserAsync(userId);

            Assert.Contains(db.Folders,
                folder =>
                    folder.IsDefault
                    && folder.Name == "Favorite Movies"
                    && folder.OwnerId == userId);
            
            Assert.Contains(db.Folders,
                folder =>
                    folder.IsDefault
                    && folder.Name == "Watch Later"
                    && folder.OwnerId == userId);
        }

        [Fact]
        public async Task CreateFolderForUser_FolderCreated()
        {
            var db = CreateAndSeedDb();
            var service = new FolderService(db);
            var userId = Guid.NewGuid().ToString();
            var folderName = "Folder";

            await service.CreateFolderForUserAsync(userId, folderName);
            
            Assert.Contains(db.Folders, folder => folder.OwnerId == userId && folder.Name == folderName);
        }
        
        [Fact]
        public async Task CreateFolderForUser_NotUniqueName_ExceptionThrown()
        {
            var db = CreateAndSeedDb();
            var service = new FolderService(db);

            await Assert.ThrowsAsync<BadRequestException>(async () =>
            {
                await service.CreateFolderForUserAsync("895973FB-F8E1-4FD6-89C4-DC13CED4780E", "Folder1");
            });
        }

        [Fact]
        public async Task DeleteFolder_FolderDeleted()
        {
            var db = CreateAndSeedDb();
            var service = new FolderService(db);

            await service.DeleteFolderAsync("895973FB-F8E1-4FD6-89C4-DC13CED4780E", 1);
            
            Assert.DoesNotContain(db.Folders, folder => folder.Id == 1);
        }

        [Fact]
        public async Task DeleteFolder_FolderDoesNotExist_ExceptionThrown()
        {
            var db = CreateAndSeedDb();
            var service = new FolderService(db);
            
            await Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
            {
                await service.DeleteFolderAsync("895973FB-F8E1-4FD6-89C4-DC13CED4780E", 4);
            });
        }
        
        [Fact]
        public async Task DeleteFolder_UserHasNoRights_ExceptionThrown()
        {
            var db = CreateAndSeedDb();
            var service = new FolderService(db);
            
            await Assert.ThrowsAsync<AccessDeniedException>(async () =>
            {
                await service.DeleteFolderAsync("895973FB-F8E1-4FD6-89C4-DC13CED4780E", 2);
            });
            
        }

        [Fact]
        public async Task RenameFolder_FolderRenamed()
        {
            var db = CreateAndSeedDb();
            var service = new FolderService(db);
            
            await service.RenameFolderAsync("895973FB-F8E1-4FD6-89C4-DC13CED4780E", 1, "New Folder");
            
            Assert.Contains(db.Folders, folder => folder.Id == 1 && folder.Name == "New Folder");
            
        }

        [Fact]
        public async Task RenameFolder_FolderDoesNotExist_ExceptionThrown()
        {
            var db = CreateAndSeedDb();
            var service = new FolderService(db);

            await Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
            {
                await service.RenameFolderAsync("895973FB-F8E1-4FD6-89C4-DC13CED4780E", 4, "New Folder");
            });
        }

        [Fact]
        public async Task RenameFolder_UserHasNoRights_ExceptionThrown()
        {
            var db = CreateAndSeedDb();
            var service = new FolderService(db);
            
            await Assert.ThrowsAsync<AccessDeniedException>(async () =>
            {
                await service.RenameFolderAsync("895973FB-F8E1-4FD6-89C4-DC13CED4780E", 2, "New Folder");
            });
        }

        [Fact]
        public async Task RenameFolder_NotUniqueName_ExceptionThrown()
        {
            var db = CreateAndSeedDb();
            var service = new FolderService(db);
            
            await Assert.ThrowsAsync<BadRequestException>(async () =>
            {
                await service.RenameFolderAsync("895973FB-F8E1-4FD6-89C4-DC13CED4780E", 1, "Folder3");
            });
        }

        [Fact]
        public async Task AddMovieToFolder_MovieAdded()
        {
            var db = CreateAndSeedDb();
            var service = new FolderService(db);

            await service.AddMovieToFolderAsync("895973FB-F8E1-4FD6-89C4-DC13CED4780E", 1, 2);

            var mf = db.MovieFolders.Last();
            Assert.NotNull(mf);
            Assert.Equal(1, mf.FolderId);
            Assert.Equal(2, mf.MovieId);
        }

        [Fact]
        public async Task AddMovieToFolder_MovieIsInFolder_ThrownException()
        {
            var db = CreateAndSeedDb();
            var service = new FolderService(db);

            await Assert.ThrowsAsync<BadRequestException>(async () =>
            {
                await service.AddMovieToFolderAsync("895973FB-F8E1-4FD6-89C4-DC13CED4780E", 1, 1);
            });

        }   

        [Fact]
        public async Task AddMovieToFolder_FolderDoesNotExist_ExceptionThrown()
        {
            var db = CreateAndSeedDb();
            var service = new FolderService(db);
            
            await Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
            {
                await service.AddMovieToFolderAsync("895973FB-F8E1-4FD6-89C4-DC13CED4780E", 4, 1);
            });
            
        }

        [Fact]
        public async Task AddMovieToFolder_UserHasNoRights_ExceptionThrown()
        {
            var db = CreateAndSeedDb();
            var service = new FolderService(db);
            
            await Assert.ThrowsAsync<AccessDeniedException>(async () =>
            {
                await service.AddMovieToFolderAsync("895973FB-F8E1-4FD6-89C4-DC13CED4780E", 2, 1);
            });
        }

        [Fact]
        public async Task DeleteMovieFromFolder_MovieDeleted()
        {
            var db = CreateAndSeedDb();
            var service = new FolderService(db);

            await service.DeleteMovieFromFolderAsync("895973FB-F8E1-4FD6-89C4-DC13CED4780E", 1, 1);
            
            Assert.DoesNotContain(db.MovieFolders, folder => folder.FolderId == 1);
        }

        [Fact]
        public async Task DeleteMovieFromFolder_MovieIsNotInFolder_ThrownException()
        {
            var db = CreateAndSeedDb();
            var service = new FolderService(db);
            
            await Assert.ThrowsAsync<BadRequestException>(async () =>
            {
                await service.DeleteMovieFromFolderAsync("895973FB-F8E1-4FD6-89C4-DC13CED4780E", 1, 2);
            });
            
        }

        [Fact]
        public async Task DeleteMovieFromFolder_UserHasNoRights_ExceptionThrown()
        {
            var db = CreateAndSeedDb();
            var service = new FolderService(db);
            
            await Assert.ThrowsAsync<AccessDeniedException>(async () =>
            {
                await service.AddMovieToFolderAsync("895973FB-F8E1-4FD6-89C4-DC13CED4780E", 2, 1);
            });
            
        }

    }
}
