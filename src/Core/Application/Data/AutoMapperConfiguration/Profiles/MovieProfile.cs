using System.Linq;
using AutoMapper;
using Core.Application.Data.QueryExtensions.Pagination;
using Core.Application.Dto;
using Core.Domain.Entities;

namespace Core.Application.Data.AutoMapperConfiguration.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDto>();
            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
            CreateMap<Movie, MovieDetailsDto>()
                .ForMember(dto => dto.Genres,
                    o =>
                    {
                        o.MapFrom(m =>
                            m.MovieGenres
                                .Select(mg => new GenreDto
                                {
                                    Id = mg.GenreId,
                                    Name = mg.Genre.Name
                                }).ToList());
                    })
                .ForMember(dto => dto.Directors,
                    o =>
                    {
                        o.MapFrom(m =>
                            m.Credit.Crew
                                .Where(c => c.Job == "Director")
                                .Select(e => new EmployeeDto
                                {
                                    Id = e.PersonId,
                                    Department = e.Department,
                                    ImagePath = e.Person.ImagePath,
                                    Job = e.Job,
                                    Name = e.Person.Name
                                })
                                .ToList());
                    });

            string userId = null;
            CreateMap<Movie, MovieWithVoteDto>()
                .ForMember(dto => dto.UserVote,
                    o => o.MapFrom(m =>
                        new VoteDto
                        {
                            UserId = userId,
                            MovieId = m.Id,
                            Watched = m.Votes.Any(v => v.UserId == userId),
                            Value = m.Votes.FirstOrDefault(v => v.UserId == userId).Value
                        }));

            string ownerId = null;
            CreateMap<Movie, MovieWithGuestVoteDto>()
                .ForMember(dto => dto.OwnerVote,
                    o => o.MapFrom(m => new VoteDto
                    {
                        UserId = ownerId,
                        MovieId = m.Id,
                        Watched = m.Votes.Any(v => v.UserId == ownerId),
                        Value = m.Votes.FirstOrDefault(v => v.UserId == ownerId).Value
                    }))
                .ForMember(dto => dto.GuestVote,
                    o => o.MapFrom(m =>
                        new VoteDto
                        {
                            UserId = userId,
                            MovieId = m.Id,
                            Watched = m.Votes.Any(v => v.UserId == userId),
                            Value = m.Votes.FirstOrDefault(v => v.UserId == userId).Value
                        }));

            CreateMap<Movie, MovieSearchItemDto>()
                .ForMember(dto => dto.Genres,
                    o =>
                    {
                        o.MapFrom(m =>
                            m.MovieGenres
                                .Select(mg => new GenreDto
                                {
                                    Id = mg.GenreId,
                                    Name = mg.Genre.Name
                                }).ToList());
                    });
        }
    }
}