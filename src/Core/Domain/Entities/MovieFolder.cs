using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    public class MovieFolder
    {
        public int MovieId { get; set; }
        public int FolderId { get; set; }

        public Movie Movie { get; set; }
        public Folder Folder { get; set; }
    }
}
