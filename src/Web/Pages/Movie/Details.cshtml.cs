using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Dto;
using Core.Application.Services.Credits;
using Core.Application.Services.Movie;
using Core.Application.Services.Votes;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models.Movie; 

namespace Web.Pages.Movie
{
    public class DetailsModel : PageModel
    {
        private readonly IMovieService _movieService;
        private readonly ICreditsService _creditsService;
        private readonly IVoteService _voteService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        private const int CastOnPage = 10;

        [BindProperty(SupportsGet = true, Name = "id")] 
        public int Id { get; set; }

        public MovieSummaryViewModel Movie { get; set; }
        public VoteDto Vote { get; set; }
        public List<ActorDto> TopCast { get; set; }

        public DetailsModel(IMovieService movieService, ICreditsService creditsService, IVoteService voteService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _movieService = movieService;
            _creditsService = creditsService;
            _voteService = voteService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var movie = await _movieService.GetMovieDetailsAsync(Id);
            if (movie == null)
            {
                return NotFound();
            }
            
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                Vote = await _voteService.GetVoteAsync(userId, Id);
            }

            TopCast = await _creditsService.GetTopCastAsync(movie.CreditId, CastOnPage);
            Movie = _mapper.Map<MovieSummaryViewModel>(movie);
            return Page();
        }
    }
}
