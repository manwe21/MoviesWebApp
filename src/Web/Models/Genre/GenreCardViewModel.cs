using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Web.Models.Genre
{
    public class GenreCardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}
