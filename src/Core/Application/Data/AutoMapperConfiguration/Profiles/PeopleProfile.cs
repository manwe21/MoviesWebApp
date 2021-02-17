using AutoMapper;
using Core.Application.Dto;
using Core.Domain.Entities;

namespace Core.Application.Data.AutoMapperConfiguration.Profiles
{
    public class PeopleProfile : Profile
    {
        public PeopleProfile()
        {
            CreateMap<Person, PersonDto>();
        }
    }
}