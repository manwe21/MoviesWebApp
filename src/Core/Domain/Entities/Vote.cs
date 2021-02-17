using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{   
    public class Vote 
    {
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public int? Value { get; set; }
        public DateTime Date { get; set; }

        public Movie Movie { get; set; }
    }
}
