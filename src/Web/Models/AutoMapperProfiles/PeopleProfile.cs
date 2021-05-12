using Application.Dto;
using AutoMapper;
using Web.Models.People;
using Web.Models.SearchTable;

namespace Web.Models.AutoMapperProfiles
{
    public class PeopleProfile : Profile
    {
        public PeopleProfile()
        {
            CreateMap<PersonDto, PersonSummaryViewModel>();
            CreateMap<PersonDto, PersonRowViewModel>();
        }
        
    }
}