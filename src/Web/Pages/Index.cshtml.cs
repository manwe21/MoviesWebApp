using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Genre;
using Application.Services.Movie;
using Application.Services.Popularity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models.Genre;
using Web.Models.Movie;
using Web.Services;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private const int UpcomingMoviesSize = 20;
        private const int PopularMoviesSize = 10;
        
        private readonly IPopularityService _popularityService;
        private readonly IMovieService _movieService;
        private readonly IGenresService _genresService;
        
        private readonly IMapper _mapper;

        public IndexModel(IPopularityService popularityService, IMovieService movieService, IGenresService genresService, IMapper mapper)
        {
            _popularityService = popularityService;
            _movieService = movieService;
            _genresService = genresService;
            _mapper = mapper;
        }

        public List<MovieCardViewModel> PopularMovies { get; set; }
        public List<MovieCardViewModel> UpcomingMovies { get; set; }
        public List<GenreCardViewModel> Genres { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var popularMovies = (await _popularityService.ListPopularMoviesAsync(PopularMoviesSize, 1)).Results;
            PopularMovies = _mapper.Map<List<MovieCardViewModel>>(popularMovies);

            var upcomingMovies = await _movieService.GetUpcomingMoviesAsync(UpcomingMoviesSize);
            UpcomingMovies = _mapper.Map<List<MovieCardViewModel>>(upcomingMovies);

            Genres = (await _genresService.ListGenresAsync())
                .Select(g => new GenreCardViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    ImagePath = $"images/genres/{g.Name.Replace(' ', '-').ToLower()}.png"
                }).ToList();

            return Page();
        }
    }
}
