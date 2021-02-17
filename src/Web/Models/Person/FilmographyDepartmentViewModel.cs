using System.Collections.Generic;
using Core.Application.Dto;

namespace Web.Models.Person
{
    public class FilmographyDepartmentViewModel
    {
        public string Department { get; set; }
        public Dictionary<int, List<FilmographyItemViewModel>> Items { get; set; }  //Grouped by year
    }
}