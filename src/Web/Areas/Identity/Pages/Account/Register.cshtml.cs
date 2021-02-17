using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.Services.Folders;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Areas.Identity.Models;

namespace Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterViewModel RegisterInput { get; set; }

        private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;

        private IFolderService _folderService;

        public RegisterModel(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IFolderService folderService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _folderService = folderService;
        }

        public string ReturnUrl { get; set; }

        public void OnGet([FromQuery]string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }
            
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    Email = RegisterInput.Email,
                    UserName = RegisterInput.Username
                };
                
                var result = await _userManager.CreateAsync(user, RegisterInput.Password);
                if (result.Succeeded)
                {
                    await _folderService.CreateDefaultFoldersForUserAsync(user.Id);
                    
                    await _signInManager.SignInAsync(user, false);
                    if (ReturnUrl != null)
                        return LocalRedirect(ReturnUrl);
                    return RedirectToPage("/Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return Page();

            }

            return Page();
        }
    }
}
