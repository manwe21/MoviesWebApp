using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.Data.Criteria;
using Core.Application.Data.QueryExtensions.Pagination;
using Core.Application.Dto;
using Core.Application.Services.Movie;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Controllers;
using Web.Models.Movie;
using Xunit;

namespace UnitTests.Web.Controllers
{
    public class MovieControllerTests : TestBase
    {
        [Fact]
        public async Task GetMovies_CorrectViewReturned()
        {
            var pagedMovies = new PagedResult<MovieDto>
            {
                AllRows = 24,
                PageNumber = 1,
                PagesCount = 2,
                PageSize = 20,
                Results = new List<MovieDto>()
            };
            var fakeMovieService = new Mock<IMovieService>();
            fakeMovieService.Setup(
                    s => s.ListMoviesAsync(It.IsAny<MovieCriteria>(),
                        It.IsAny<string>(),
                        It.IsAny<int>(),
                        It.IsAny<int>()))
                .ReturnsAsync(pagedMovies);
            
            var controller = new MovieController(fakeMovieService.Object, Mapper);
            
            var res = (PartialViewResult) await controller.GetMovies(new MovieCriteria());
            
            var model = (MovieListPagedViewModel)res.Model;
            Assert.Equal(1, model.PageNumber);
            Assert.Equal(2, model.PagesCount);
            Assert.Equal(24, model.RowsCount);
            Assert.Equal(20, model.PageSize);
            Assert.NotNull(model.Movies);
        }
    }
}