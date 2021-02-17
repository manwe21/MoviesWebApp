using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Services.Votes;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Filters;

namespace Web.Controllers
{
    [AjaxAuthentication]
    public class VoteController : BaseController
    {
        private readonly IVoteService _voteService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<VoteController> _logger;
        private readonly IMapper _mapper;

        public VoteController(IVoteService voteService, UserManager<AppUser> userManager, ILogger<VoteController> logger, IMapper mapper)
        {
            _voteService = voteService;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }
        
        [HttpPost("rate")]
        public async Task<IActionResult> RateMovie(int movieId, int value)
        {
            var userId = _userManager.GetUserId(User);
            await _voteService.RateMovieAsync(userId, movieId, value);
            return Ok();
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> AddMovieToWatched(int movieId)
        {
            var userId = _userManager.GetUserId(User);
            await _voteService.MarkMovieAsWatchedAsync(userId, movieId);
            return Ok();
        }
        
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveMovieFromWatched(int movieId)
        {
            var userId = _userManager.GetUserId(User);
            await _voteService.UnmarkMovieAsWatchedAsync(userId, movieId);
            return Ok();
        }
        
        [HttpGet("rateMenu")]
        public async Task<IActionResult> GetVoteMenu(int movieId)
        {
            var userId = _userManager.GetUserId(User);
            var vote = await _voteService.GetVoteAsync(userId, movieId);
            return PartialView("Movie/VoteMenu", vote);
        }
    }
}
