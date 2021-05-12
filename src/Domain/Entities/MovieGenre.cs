﻿using Domain.Common;

namespace Domain.Entities
{
    public class MovieGenre : BaseEntity
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
