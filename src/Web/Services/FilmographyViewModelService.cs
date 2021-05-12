using System.Collections.Generic;
using System.Linq;
using Application.Dto;
using Web.Models.Person;

namespace Web.Services
{
    public class FilmographyViewModelService : IFilmographyViewModelService
    {
        public List<FilmographyDepartmentViewModel> GetFilmographyViewModel(List<FilmographyItemDto> filmography)
        {
            var model = new List<FilmographyDepartmentViewModel>();
            
            //group by department
            var byDepartment = filmography
                .GroupBy(k => k.Department, el => new
                    {
                        el.Title,
                        el.MovieId,
                        el.PersonPositions,
                        el.ReleaseDate.Year
                    })
                .ToList();

            //then group by year
            var byYear = byDepartment
                .Select(g => new
                {
                    g.Key,
                    ByYear = g.GroupBy(k => k.Year, el => new 
                        {
                            el.Title,
                            el.MovieId,
                            el.PersonPositions
                        }).ToList()
                }).ToList();

            foreach (var bd in byYear)
            {
                var bdItem = new FilmographyDepartmentViewModel
                {
                    Department = bd.Key,
                    Items = new Dictionary<int, List<FilmographyItemViewModel>>()
                };
                foreach (var by in bd.ByYear)
                {
                    bdItem.Items.Add(
                        by.Key,
                        by.Select(y => new FilmographyItemViewModel
                        {
                            MovieId = y.MovieId,
                            Title = y.Title,
                            PersonPositions = y.PersonPositions
                        }).ToList());
                }
                model.Add(bdItem);
            }
            
            return model;
        }
    }
}
