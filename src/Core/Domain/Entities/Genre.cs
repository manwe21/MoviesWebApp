using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Domain.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }

        public List<MovieGenre> MovieGenres { get; set; }

        public Genre()
        {
            MovieGenres = new List<MovieGenre>();
        }
    }
}
