using System.Collections.Generic;
using Application.Dto;
using Web.Models.Person;

namespace Web.Services
{
    public interface IFilmographyViewModelService
    {
        public List<FilmographyDepartmentViewModel> GetFilmographyViewModel(List<FilmographyItemDto> filmography);
    }
}
