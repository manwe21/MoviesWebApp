using System.Collections.Generic;
using System.Linq;
using Application.Data.QueryExtensions.Pagination;
using Application.Dto;
using AutoMapper;
using Web.Models.Folder;
using Web.Models.Movie;
using Web.Models.Search;

namespace Web.Models.AutoMapperProfiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieDto, MovieCardViewModel>();
            CreateMap<MovieDetailsDto, MovieSummaryViewModel>()
                .ForMember(vm => vm.Genres, e => e.MapFrom(dto => dto.Genres.Select(g => g.Name).ToList()));
            CreateMap<PagedResult<MovieDto>, MovieListPagedViewModel>()
                .ForMember(vm => vm.RowsCount, e => e.MapFrom(dto => dto.AllRows))
                .AfterMap((src, dest, context) => dest.Movies = context.Mapper.Map<List<MovieDto>, List<MovieCardViewModel>>(src.Results));

            CreateMap<MovieWithVoteDto, MovieWithVoteViewModel>()
                .ForMember(vm => vm.OwnerVote, e => e.MapFrom(dto => dto.UserVote))
                .ForMember(vm => vm.GuestVote, e => e.Ignore());
            CreateMap<MovieWithGuestVoteDto, MovieWithVoteViewModel>();
            
        }
    }
}
