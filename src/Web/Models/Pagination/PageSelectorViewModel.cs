using System;

namespace Web.Models.Pagination
{
    public class PageSelectorViewModel
    {
        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }
        public Func<int, string> Url { get; set; }
    }
}