using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Data
{
    [Keyless]
    public class SearchItem
    {
        public int Key { get; set; }
        public int Rank { get; set; }
    }
}
