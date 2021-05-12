using Application.Data.QueryExtensions.Pagination;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Api.JsonObjects;

namespace Infrastructure.Data.AutoMapperProfiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap(typeof(PagedResultJson<>), typeof(PagedResult<>))
                .ForMember("PageSize", expression => expression.Ignore());
            CreateMap<MovieJson, Movie>();
        }
    }
}
