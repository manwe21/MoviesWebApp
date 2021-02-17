using Core.Application.Data.QueryExtensions.Pagination;
using Web.Models.Movie;
using Web.Pages.User;

namespace Web.Models.User
{
    public class UserMoviesTableViewModel
    {
        public PagedResult<MovieWithVoteViewModel> Movies { get; set; }
        public RequestInitiator RequestInitiator { get; set; }
    }
}
