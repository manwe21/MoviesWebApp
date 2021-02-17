using AutoMapper;
using Core.Application.Dto;
using Core.Domain.Entities;

namespace Core.Application.Data.AutoMapperConfiguration.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreDto>();
        }
    }
}