using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Dto
{
    public class VoteDto
    {
        public string UserId { get; set; }  
        public int MovieId { get; set; }
        public bool Watched { get; set; }
        public int? Value { get; set; }
    }
}
