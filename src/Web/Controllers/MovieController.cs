using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Data.Criteria;
using Core.Application.Services.Movie;
using Microsoft.AspNetCore.Mvc;
using Web.Attributes;
using Web.Models.Movie;

namespace Web.Controllers
{
    public class MovieController : BaseController
    {
        private const int PageSize = 20;
        
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }   

        [AjaxOnly]
        [HttpGet("list/{pageNumber?}")]
        public async Task<IActionResult> GetMovies(MovieCriteria filters, string orderBy="Rating.desc", int pageNumber = 1)
        {
            var movies = await _movieService.ListMoviesAsync(filters, orderBy, PageSize, pageNumber);
            var model = _mapper.Map<MovieListPagedViewModel>(movies);
            return PartialView("MovieCardList", model);
        }
        
    }
}   

