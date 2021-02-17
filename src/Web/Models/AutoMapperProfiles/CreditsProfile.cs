using AutoMapper;
using Core.Application.Dto;
using Web.Models.Movie;
using Web.Models.Person;

namespace Web.Models.AutoMapperProfiles
{
    public class CreditsProfile : Profile
    {
        public CreditsProfile()
        {
            CreateMap<ActorDto, ActorViewModel>()
                .ForMember(vm => vm.Position, o => o.MapFrom(a => a.Character));
            CreateMap<EmployeeDto, ActorViewModel>()
                .ForMember(vm => vm.Position, o => o.MapFrom(e => e.Job));

        }
    }
}