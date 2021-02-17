using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Data.QueryExtensions.Pagination;
using Core.Application.Dto;
using Core.Application.Exceptions;
using Core.Application.Services.User;
using Core.Application.Services.Votes;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models.Movie;

namespace Web.Pages.User
{
    public class MoviesModel : UserPageModel
    {
        private const int MinMutualVotesCount = 20;
        private const int MinActorMovies = 5;
        private const int ActorsCount = 20;
        private const int MoviesPageSize = 10;
        
        private readonly IUserService _userService;
        private readonly IVoteService _voteService;
        private readonly UserManager<AppUser> _userManager;
        
        private readonly IMapper _mapper;
        private ILogger<MoviesModel> _logger;

        public PagedResult<MovieWithVoteViewModel> Movies { get; set; }
        public List<UserActorDto> Actors { get; set; }
        public int VotesSimilarity { get; set; }
        public bool IsVotesNotEnough { get; set; }

        public MoviesModel(IUserService userService, IVoteService voteService, UserManager<AppUser> userManager, ILogger<MoviesModel> logger, IMapper mapper)
        {
            _voteService = voteService;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync([FromQuery(Name = "page_n")] int pageNumber = 1)
        {
            var profileOwner = await _userManager.FindByNameAsync(Username);
            if (profileOwner == null)
                return NotFound();
            
            Actors = await _userService.GetActorsAsync(profileOwner.Id, MinActorMovies, ActorsCount);

            switch (RequestInitiator)
            {
                case RequestInitiator.Owner:
                case RequestInitiator.Unknown:
                    var movies = await _userService.ListMoviesAsync(profileOwner.Id, pageNumber, MoviesPageSize);
                    Movies = _mapper.Map<PagedResult<MovieWithVoteViewModel>>(movies);
                    return Page();
                
                case RequestInitiator.Guest:
                    var guestId = _userManager.GetUserId(User);
                    var result = await _userService.ListMoviesForCoupleAsync(profileOwner.Id, guestId, pageNumber, MoviesPageSize);
                    Movies = _mapper.Map<PagedResult<MovieWithVoteViewModel>>(result);
                    try
                    {
                        VotesSimilarity = await _voteService.GetVotesSimilarityAsync(profileOwner.Id, guestId, MinMutualVotesCount);
                    }
                    catch (NotEnoughVotesException)
                    {
                        IsVotesNotEnough = true;
                    }
                    return Page();
            }
            
            return Page();
        }
    }
}
