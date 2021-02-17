using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Data.Criteria;
using Core.Application.Dto;
using Core.Application.Services.Credits;
using Core.Application.Services.Movie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models.Movie;
using Web.Models.Person;

namespace Web.Pages.Movie
{
    public class PeopleModel : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "id")]
        public int Id { get; set; }

        private readonly IMovieService _movieService;
        private readonly ICreditsService _creditsService;
        private readonly IMapper _mapper;
        
        public string Title { get; set; }
        public List<ActorViewModel> Cast { get; set; }
        public List<ActorViewModel> Crew { get; set; }
            

        public PeopleModel(IMovieService movieService, ICreditsService creditsService, IMapper mapper)
        {
            _movieService = movieService;
            _creditsService = creditsService;
            _mapper = mapper;
        }   

        public async Task<IActionResult> OnGetAsync()
        {
            var movie = await _movieService.GetMovieAsync(Id);
            Title = movie.Title;

            Cast = _mapper.Map<List<ActorViewModel>>(await _creditsService.GetTopCastAsync(movie.CreditId));
            Crew = _mapper.Map<List<ActorViewModel>>(await _creditsService.GetCrewAsync(movie.CreditId, new CrewCriteria()));

            return Page();
        }
    }
}