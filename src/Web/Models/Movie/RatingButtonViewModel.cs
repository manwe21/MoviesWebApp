using Core.Application.Dto;

namespace Web.Models.Movie
{
    public class RatingButtonViewModel
    {
        public bool IsActive { get; set; }
        public VoteDto Vote { get; set; }
    }
}
