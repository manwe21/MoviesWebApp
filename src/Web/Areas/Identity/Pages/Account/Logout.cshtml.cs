using System.Threading.Tasks;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Areas.Identity.Pages.Account
{
    public class Logout : PageModel
    {
        private SignInManager<AppUser> _signInManager;

        public Logout(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet(string returnUrl = null)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Index");
            
            await _signInManager.SignOutAsync();

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            return RedirectToPage("/Index");
        }
    }
}
