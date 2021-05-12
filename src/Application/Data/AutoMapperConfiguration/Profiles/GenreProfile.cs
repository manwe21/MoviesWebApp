using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Data.AutoMapperConfiguration.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreDto>();
        }
    }
}