using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Domain.Entities
{
    public class MovieGenre
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
