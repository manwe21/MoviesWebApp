using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.Data.QueryExtensions.Pagination;
using Core.Application.Dto;
using Core.Application.Services.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Web.Controllers;
using Web.Models.Search;
using Xunit;

namespace UnitTests.Web.Controllers
{
    public class SearchControllerTests : TestBase
    {
        private PagedResult<PersonDto> GetPeople()
        {
            return new PagedResult<PersonDto>
            {
                Results = new List<PersonDto>
                {
                    new PersonDto
                    {
                        Id = 1,
                        ImagePath = "",
                        Name = "Name1"
                    },
                    new PersonDto
                    {
                        Id = 2,
                        ImagePath = "",
                        Name = "Name2"
                    }
                }
            };
        }

        private PagedResult<MovieSearchItemDto> GetMovies()
        {
            return new PagedResult<MovieSearchItemDto>
            {
                Results = new List<MovieSearchItemDto>
                {
                    new MovieSearchItemDto
                    {
                        Id = 1,
                        Title = "Movie",
                        Genres = new List<GenreDto>
                        {
                            new GenreDto
                            {
                                Id = 1, Name = "Drama"
                            },
                            new GenreDto
                            {
                                Id = 2, Name = "Adventure"
                            }
                        },
                        PosterPath = ""
                    }
                }
            };
        }
        
        [Fact]
        public async Task GetSearchDropdown()
        {
            var fakeSearchService = new Mock<ISearchService>();
            fakeSearchService
                .Setup(s =>
                    s.SearchMoviesAsync(
                        It.IsAny<string>(),
                        It.IsAny<int>(),
                        It.IsAny<int>()))
                .ReturnsAsync(GetMovies());
            
            fakeSearchService
                .Setup(s =>
                    s.SearchPeopleAsync(
                        It.IsAny<string>(),
                        It.IsAny<int>(),
                        It.IsAny<int>()))
                .ReturnsAsync(GetPeople());

            
            var logger = Mock.Of<ILogger<SearchController>>();
            var controller = new SearchController(fakeSearchService.Object, logger, Mapper);

            var res = await controller.GetSearchDropdown("query");
            Assert.IsType<PartialViewResult>(res);

            var viewResult = res as PartialViewResult;
            Assert.IsType<SearchDropdownViewModel>(viewResult.Model);
            
            var model = (SearchDropdownViewModel)viewResult.Model;
            Assert.Equal("query", model.Query);
            Assert.Equal(3, model.SearchItems.Count);
        }
    }
}