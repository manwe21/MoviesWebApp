using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Dto
{
    public class UserActorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public double AverageRating { get; set; }
        public int MoviesCount { get; set; }
    }
}
