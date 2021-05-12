using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Data.AutoMapperConfiguration.Profiles
{
    public class PeopleProfile : Profile
    {
        public PeopleProfile()
        {
            CreateMap<Person, PersonDto>();
        }
    }
}