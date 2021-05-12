using System.Collections.Generic;

namespace Web.Models.Person
{
    public class FilmographyDepartmentViewModel
    {
        public string Department { get; set; }
        public Dictionary<int, List<FilmographyItemViewModel>> Items { get; set; }  //Grouped by year
    }
}