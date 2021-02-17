using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Dto;
using Microsoft.Extensions.Logging;

namespace Core.Application.Services.People
{
    public class PeopleService : IPeopleService
    {
        private readonly IMovieContext _db;
        private readonly IMapper _mapper;

        public PeopleService(IMovieContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PersonDto> GetPersonAsync(int personId)
        {
            var person = await _db.People.FindAsync(personId);
            return _mapper.Map<PersonDto>(person);
        }
    }
}
