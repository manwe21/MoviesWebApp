using Application.Dto;
using AutoMapper;
using Web.Models.Folder;

namespace Web.Models.AutoMapperProfiles
{
    public class FolderProfile : Profile
    {
        public FolderProfile()
        {
            CreateMap<FolderDto, FolderViewModel>();
        }
    }
}