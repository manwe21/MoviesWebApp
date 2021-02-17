using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.User
{
    public class UserPageModel : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "initiator")]
        public RequestInitiator RequestInitiator { get; set; }

        [BindProperty(SupportsGet = true, Name = "username")]
        public string Username { get; set; }


    }
}
