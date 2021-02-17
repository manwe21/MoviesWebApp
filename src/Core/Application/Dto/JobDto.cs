using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Dto
{   
    public class JobDto    
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }

    }    
}
