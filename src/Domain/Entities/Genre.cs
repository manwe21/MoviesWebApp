using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
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
