using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Folder
{
    public class FolderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; } 
        public int MoviesCount { get; set; }
    }
}
    