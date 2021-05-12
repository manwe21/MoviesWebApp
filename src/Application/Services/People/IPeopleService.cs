using System.Threading.Tasks;
using Application.Dto;

namespace Application.Services.People
{   
    public interface IPeopleService     
    {
        Task<PersonDto> GetPersonAsync(int peronId); 

    }
}
