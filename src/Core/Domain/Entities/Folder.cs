using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class Folder : BaseEntity
    {
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public bool IsDefault { get; set; }

        public List<MovieFolder> MovieFolders { get; set; }

    }
}
