using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dto;
using Application.Services.Folders;
using AutoMapper;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Web.Pages.User;
using Xunit;

namespace UnitTests.Web.Pages.User
{
    public class FoldersModelTests : TestBase
    {
        [Fact]
        public async Task GetAsync_ProfileOwnerNotFound_404Returned()
        {
            var fakeUserStore = new Mock<IUserStore<AppUser>>();
            var fakeUserManager = new Mock<UserManager<AppUser>>
                (fakeUserStore.Object, null, null, null, null, null, null, null, null);
            fakeUserManager.Setup(m => m.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            var fakeFolderService = new Mock<IFolderService>();
            
            var moviesModel = new FoldersModel(fakeFolderService.Object, fakeUserManager.Object, Mapper);

            var result = await moviesModel.OnGetAsync(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAsync_FolderIdIsNotDefined_CurrentFolderIsFirstFolder()
        {
            var pageContext = CreatePageContext(false);
            
            var fakeUserStore = new Mock<IUserStore<AppUser>>();
            var fakeUserManager = new Mock<UserManager<AppUser>>
                (fakeUserStore.Object, null, null, null, null, null, null, null, null);
            fakeUserManager.Setup(m => m.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(() => new AppUser());

            var fakeFolderService = new Mock<IFolderService>();
            fakeFolderService.Setup(s => s.ListFoldersForUserAsync(It.IsAny<string>()))
                .ReturnsAsync(GetFolders);

            var foldersModel = new FoldersModel(fakeFolderService.Object, fakeUserManager.Object, Mapper)
            {
                PageContext = pageContext
            };

            await foldersModel.OnGetAsync(null);
            
            Assert.Equal(1, foldersModel.CurrentFolder.Id);
        }

        [Fact]
        public async Task GetAsync_FolderOwnerNotFound_404Returned()
        {
            var pageContext = CreatePageContext(false);
            
            var fakeUserStore = new Mock<IUserStore<AppUser>>();
            var fakeUserManager = new Mock<UserManager<AppUser>>
                (fakeUserStore.Object, null, null, null, null, null, null, null, null);
            fakeUserManager.Setup(m => m.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(() => new AppUser());

            var fakeFolderService = new Mock<IFolderService>();
            fakeFolderService.Setup(s => s.ListFoldersForUserAsync(It.IsAny<string>()))
                .ReturnsAsync(GetFolders);

            var foldersModel = new FoldersModel(fakeFolderService.Object, fakeUserManager.Object, Mapper)
            {
                PageContext = pageContext
            };

            var result = await foldersModel.OnGetAsync(3);
            
            Assert.IsType<NotFoundResult>(result);
        }

        private List<FolderDto> GetFolders()
        {
            return new List<FolderDto>
            {
                new FolderDto
                {
                    Id = 1
                },
                new FolderDto
                {
                    Id = 2
                }
            };
        }

    }
}