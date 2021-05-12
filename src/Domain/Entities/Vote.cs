using System;
using Domain.Common;

namespace Domain.Entities
{   
    public class Vote : BaseEntity
    {
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public int? Value { get; set; }
        public DateTime Date { get; set; }

        public Movie Movie { get; set; }
    }
}
