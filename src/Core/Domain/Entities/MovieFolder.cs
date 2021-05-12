using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain.Common;

namespace Core.Domain.Entities
{
    public class MovieFolder : BaseEntity
    {
        public int MovieId { get; set; }
        public int FolderId { get; set; }

        public Movie Movie { get; set; }
        public Folder Folder { get; set; }
    }
}
