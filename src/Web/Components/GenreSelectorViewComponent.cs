using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Genre;
using Microsoft.AspNetCore.Mvc;

namespace Web.Components
{
    public class GenreSelectorViewComponent : ViewComponent
    {
        private readonly IGenresService _service;

        public GenreSelectorViewComponent(IGenresService service)
        {
            _service = service; 
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = await _service.ListGenresAsync();
            return View(genres);
        }

    }
}
