using System.Threading.Tasks;
using Core.Application.Services.Credits;
using Core.Application.Services.Movie;
using Core.Application.Services.People;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Pages.Person;
using Web.Services;
using Xunit;

namespace UnitTests.Web.Pages.Person
{
    public class DetailsModelTests : TestBase
    {
        [Fact]
        public async Task GetAsync_PersonDoesNotExist_404Returned()
        {
            var fakeMovieService = new Mock<IMovieService>();
            var fakeCreditsService = new Mock<ICreditsService>();
            var fakeFilmographyService = new Mock<IFilmographyViewModelService>();
            var fakePeopleService = new Mock<IPeopleService>();
            fakePeopleService.Setup(s => s.GetPersonAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            var model = new PersonModel(fakeMovieService.Object, fakePeopleService.Object, fakeCreditsService.Object, Mapper, fakeFilmographyService.Object);

            var result = await model.OnGetAsync();

            Assert.IsType<NotFoundResult>(result);

        }        
    }
}
