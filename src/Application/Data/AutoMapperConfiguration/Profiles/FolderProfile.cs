using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Data.AutoMapperConfiguration.Profiles
{
    public class FolderProfile : Profile
    {
        public FolderProfile()
        {
            CreateMap<Folder, FolderDto>()
                .ForMember(dto => dto.MoviesCount,
                    e => e.MapFrom(f => f.MovieFolders.Count))
                .ForMember(dto => dto.Name, o => o.MapFrom(f => f.Name.Value));
        }
    }
}