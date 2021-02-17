using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.Data.QueryExtensions.Pagination;
using Core.Application.Dto;
using Core.Application.Exceptions;
using Core.Application.Services.User;
using Core.Application.Services.Votes;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Web.Pages.User;
using Xunit;

namespace UnitTests.Web.Pages.User
{
    public class MoviesModelTests : TestBase
    {
        [Fact]
        public async Task GetAsync_ProfileOwnerNotFound_404Returned()
        {
            var fakeUserStore = new Mock<IUserStore<AppUser>>();
            var fakeUserManager = new Mock<UserManager<AppUser>>(fakeUserStore.Object, null, null, null, null, null, null, null, null);
            fakeUserManager.Setup(m => m.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(() => null);
            
            var fakeUserService = new Mock<IUserService>();
            var fakeVoteService = new Mock<IVoteService>();
            var fakeLogger = new Mock<ILogger<MoviesModel>>();
            
            var moviesModel = new MoviesModel(fakeUserService.Object, fakeVoteService.Object, fakeUserManager.Object, fakeLogger.Object, Mapper);

            var result = await moviesModel.OnGetAsync();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAsync_MutualVotesIsNotEnoughExceptionThrown_IsVotesNotEnoughTrue()
        {
            var pageContext = CreatePageContext(true);
            
            var fakeUserStore = new Mock<IUserStore<AppUser>>();
            var fakeUserManager = new Mock<UserManager<AppUser>>(fakeUserStore.Object, null, null, null, null, null, null, null, null);
            fakeUserManager.Setup(m => m.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new AppUser
                {
                    Id = "id"
                });
            var fakeUserService = new Mock<IUserService>();

            var fakeVoteService = new Mock<IVoteService>();
            fakeVoteService
                .Setup(s => s.GetVotesSimilarityAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .ThrowsAsync(new NotEnoughVotesException(It.IsAny<int>()));
            
            var fakeLogger = new Mock<ILogger<MoviesModel>>();
            var moviesModel = new MoviesModel(fakeUserService.Object, fakeVoteService.Object, fakeUserManager.Object, fakeLogger.Object, Mapper)
            {
                RequestInitiator = RequestInitiator.Guest,
                PageContext = pageContext
            };
            
            await moviesModel.OnGetAsync();
            
            Assert.True(moviesModel.IsVotesNotEnough);
        }
        
    }
}
