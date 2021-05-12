using System.Collections.Generic;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Folder : BaseEntity
    {
        public int Id { get; set; }
        public FolderName Name { get; set; }
        public string OwnerId { get; set; }
        public bool IsDefault { get; set; }

        public List<MovieFolder> MovieFolders { get; set; }

    }
}
