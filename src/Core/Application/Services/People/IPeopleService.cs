using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Application.Dto;

namespace Core.Application.Services.People
{   
    public interface IPeopleService     
    {
        Task<PersonDto> GetPersonAsync(int peronId); 

    }
}
