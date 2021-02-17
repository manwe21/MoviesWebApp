using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Identity.Models
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        public string PasswordConfirm { get; set; }

    }
}   
