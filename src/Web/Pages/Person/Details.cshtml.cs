using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Credits;
using Application.Services.Movie;
using Application.Services.People;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models.Movie;
using Web.Models.People;
using Web.Models.Person;
using Web.Services;

namespace Web.Pages.Person
{   
    public class PersonModel : PageModel
    {
        private readonly IMovieService _movieService;
        private readonly IPeopleService _peopleService;
        private readonly ICreditsService _creditsService;
        private readonly IFilmographyViewModelService _filmographyService;
        private readonly IMapper _mapper;

        [BindProperty(SupportsGet = true, Name = "id")]
        public int PersonId { get; set; }
        
        public List<MovieCardViewModel> FamousMovies { get; set; }
        public PersonSummaryViewModel Person { get; set; }
        public List<FilmographyDepartmentViewModel> Filmography { get; set; }
        
        public PersonModel(IMovieService movieService, IPeopleService peopleService, ICreditsService creditsService, IMapper mapper, IFilmographyViewModelService filmographyService)
        {
            _movieService = movieService;
            _peopleService = peopleService;
            _creditsService = creditsService;
            _filmographyService = filmographyService;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            FamousMovies = _mapper.Map<List<MovieCardViewModel>>(await _movieService.GetFamousMoviesForPersonAsync(PersonId, 10));
            var person = await _peopleService.GetPersonAsync(PersonId);
            if (person == null)
                return new NotFoundResult();
            
            Person = _mapper.Map<PersonSummaryViewModel>(person);
            
            var filmography = await _creditsService.GetPersonFilmographyAsync(PersonId);
            Filmography = _filmographyService.GetFilmographyViewModel(filmography);
            return Page();
        }

    }
}

