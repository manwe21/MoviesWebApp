using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using Core.Domain.Common;

namespace Core.Domain.Entities
{
    public class Genre : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<MovieGenre> MovieGenres { get; set; }

        public Genre()
        {
            MovieGenres = new List<MovieGenre>();
        }
    }
}
