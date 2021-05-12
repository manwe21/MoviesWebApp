using Microsoft.EntityFrameworkCore;

namespace Application.Data
{
    [Keyless]
    public class SearchItem
    {
        public int Key { get; set; }
        public int Rank { get; set; }
    }
}
