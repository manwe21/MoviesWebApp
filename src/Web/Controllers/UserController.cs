using System.Threading.Tasks;
using Core.Application.Services.User;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public UserController(IUserService userService, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _userService = userService;
        }
        
        [HttpGet("{username}/genres")]
        public async Task<IActionResult> GetGenres(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return NotFound();
            
            var id = user.Id;
            var genres = await _userService.GetGenresAsync(id);
            return Json(genres);
        }
        
        [HttpGet("{username}/actors")]
        public async Task<IActionResult> GetActors(string username)
        {   
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return NotFound();
            
            var id = user.Id;
            var actors = await _userService.GetActorsAsync(id, 5, 15);
            return Json(actors);
        }
        
        [HttpGet("{username}/years")]
        public async Task<IActionResult> GetYears(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return NotFound();
            
            var id = user.Id;
            var years = await _userService.GetYearsAsync(id);
            return Json(years);
        }

    }
}
