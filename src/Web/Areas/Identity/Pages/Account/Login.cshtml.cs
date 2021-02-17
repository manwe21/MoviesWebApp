using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Areas.Identity.Models;

namespace Web.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private SignInManager<AppUser> _signInManager;
        
        [BindProperty]
        public LoginViewModel LoginInput { get; set; }
        public string ReturnUrl { get; set; }

        public LoginModel(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }
            
        public void OnGet([FromQuery]string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(LoginInput.Name, LoginInput.Password, LoginInput.RememberMe, false);
                if (result.Succeeded)
                {
                    if (ReturnUrl != null)
                        return LocalRedirect(ReturnUrl);
                    return RedirectToPage("/Index");
                }

                ModelState.AddModelError("", "Wrong username or password");
                return Page();
            }

            return Page();
        }

    }
}
