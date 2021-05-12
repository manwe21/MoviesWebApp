using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using Application.Dto;
using Application.Services.Credits;
using Application.Services.Movie;
using Application.Services.Votes;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using Web.Pages.Movie;
using Xunit;

namespace UnitTests.Web.Pages.Movie
{
    public class DetailsModelTests : TestBase
    {
        [Fact]
        public async Task GetAsync_MovieNotFound_404Returned()
        {
            var fakeMovieService = new Mock<IMovieService>();
            fakeMovieService.Setup(s => s.GetMovieDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            var model = new DetailsModel(fakeMovieService.Object, null, null, null, null);

            var result = await model.OnGetAsync();
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAsync_UserAuthenticated_VoteIsNotNull()
        {
            var fakeContext = new Mock<HttpContext>();
            var fakeIdentity = new Mock<IIdentity>();
            fakeIdentity.Setup(i => i.IsAuthenticated).Returns(true);
            var principal = new GenericPrincipal(fakeIdentity.Object, null);
            fakeContext.Setup(x => x.User).Returns(principal);
            var pageContext = new PageContext
            {
                HttpContext = fakeContext.Object
            };
            
            var fakeMovieService = new Mock<IMovieService>();
            fakeMovieService.Setup(s => s.GetMovieDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(new MovieDetailsDto());
            
            var creditsService = new Mock<ICreditsService>();
            creditsService.Setup(s => s.GetTopCastAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<ActorDto>());
            
            var fakeVoteService = new Mock<IVoteService>();
            fakeVoteService.Setup(s => s.GetVoteAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new VoteDto());

            var fakeUserStore = new Mock<IUserStore<AppUser>>();
            var fakeUserManager = new UserManager<AppUser>(fakeUserStore.Object, null, null, null, null, null, null, null, null);
            
            var model = new DetailsModel(fakeMovieService.Object, creditsService.Object, fakeVoteService.Object,
                fakeUserManager, Mapper);
            model.PageContext = pageContext;

            await model.OnGetAsync();
            Assert.NotNull(model.Vote);

        }
    }
}
