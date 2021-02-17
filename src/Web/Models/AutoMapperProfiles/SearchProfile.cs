using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Application.Data.QueryExtensions.Pagination;
using Core.Application.Dto;
using Web.Models.Search;
using Web.Models.SearchTable;

namespace Web.Models.AutoMapperProfiles
{
    public class SearchProfile : Profile
    {
        public SearchProfile()
        {
            CreateMap<PersonDto, SearchItemViewModel>()
                .ForMember(s => s.SearchCategory, e => e.MapFrom(p => SearchCategory.People));
            
            CreateMap<MovieSearchItemDto, MovieRowViewModel>()
                .ForMember(r => r.Name, o => o.MapFrom(m => m.Title))
                .ForMember(r => r.ImagePath, o => o.MapFrom(m => m.PosterPath))
                .ForMember(r => r.Genres, o => o.MapFrom(m => m.Genres.Select(g => g.Name)))
                .ForMember(r => r.Year, o => o.MapFrom(m => m.ReleaseDate.Year));
            
            CreateMap<MovieSearchItemDto, SearchItemViewModel>()
                .ForMember(s => s.SearchCategory, e => e.MapFrom(m => SearchCategory.Movies))
                .ForMember(s => s.Name, e => e.MapFrom(m => m.Title))
                .ForMember(s => s.ImagePath, e => e.MapFrom(m => m.PosterPath))
                .ForMember(s => s.Year, o => o.MapFrom(m => m.ReleaseDate.Year));

            CreateMap<PagedResult<MovieSearchItemDto>, PagedResult<BaseRowViewModel>>()
                .ForMember(dest => dest.Results, (o) => o.MapFrom((src, dest, item, ctx) =>
                    ctx.Mapper.Map<List<MovieRowViewModel>>(src.Results)
                    .Cast<BaseRowViewModel>()
                    .ToList()));
            
            CreateMap<PagedResult<PersonDto>, PagedResult<BaseRowViewModel>>()
                .ForMember(dest => dest.Results, o => o.MapFrom((src, dest, item, ctx) =>
                    ctx.Mapper.Map<List<PersonRowViewModel>>(src.Results)
                    .Cast<BaseRowViewModel>()
                    .ToList()));
        }
    }
}