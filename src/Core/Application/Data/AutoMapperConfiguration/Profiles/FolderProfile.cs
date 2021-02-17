using AutoMapper;
using Core.Application.Dto;
using Core.Domain.Entities;

namespace Core.Application.Data.AutoMapperConfiguration.Profiles
{
    public class FolderProfile : Profile
    {
        public FolderProfile()
        {
            CreateMap<Folder, FolderDto>()
                .ForMember(dto => dto.MoviesCount,
                    e => e.MapFrom(f => f.MovieFolders.Count));
        }
    }
}