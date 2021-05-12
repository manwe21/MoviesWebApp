using System.Collections.Generic;
using Core.Domain.Common;

namespace Core.Domain.Entities
{
    public class Folder : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public bool IsDefault { get; set; }

        public List<MovieFolder> MovieFolders { get; set; }

    }
}
