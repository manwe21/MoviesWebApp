using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Data.AutoMapperConfiguration.Profiles
{
    public class CreditsProfile : Profile
    {
        public CreditsProfile()
        {
            CreateMap<Role, ActorDto>()
                .ForMember(a => a.Name,
                    e => e.MapFrom(r => r.Actor.Name))
                .ForMember(a => a.Id,
                    e => e.MapFrom(r => r.ActorId))
                .ForMember(a => a.Character,
                    e => e.MapFrom(r => r.Character))
                .ForMember(a => a.ImagePath,
                    e => e.MapFrom(r => r.Actor.ImagePath));

            CreateMap<Employee, EmployeeDto>()
                .ForMember(dto => dto.ImagePath,
                    e => e.MapFrom(emp => emp.Person.ImagePath))
                .ForMember(a => a.Id,
                    e => e.MapFrom(r => r.PersonId))
                .ForMember(dto => dto.Name,
                    e => e.MapFrom(emp => emp.Person.Name));

            CreateMap<Role, JobDto>()
                .ForMember(j => j.MovieId, 
                    e => e.MapFrom(r => r.Credit.Movie.Id))
                .ForMember(j => j.Position,
                    e => e.MapFrom(r => r.Character))
                .ForMember(j => j.ReleaseDate,
                    e => e.MapFrom(r => r.Credit.Movie.ReleaseDate))
                .ForMember(j => j.Title,
                    e => e.MapFrom(r => r.Credit.Movie.Title))
                .ForMember(j => j.Department, 
                    e => e.MapFrom(r => "Acting"));
            
            CreateMap<Employee, JobDto>()
                .ForMember(j => j.MovieId, 
                    e => e.MapFrom(emp => emp.Credit.Movie.Id))
                .ForMember(j => j.Position,
                    e => e.MapFrom(emp => emp.Job))
                .ForMember(j => j.ReleaseDate,
                    e => e.MapFrom(emp => emp.Credit.Movie.ReleaseDate))
                .ForMember(j => j.Title,
                    e => e.MapFrom(emp => emp.Credit.Movie.Title))
                .ForMember(j => j.Department, 
                    e => e.MapFrom(emp => emp.Department));

        }
    }
}