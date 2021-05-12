using Domain.Common;

namespace Domain.Entities
{
    public class MovieFolder : BaseEntity
    {
        public int MovieId { get; set; }
        public int FolderId { get; set; }

        public Movie Movie { get; set; }
        public Folder Folder { get; set; }
    }
}
