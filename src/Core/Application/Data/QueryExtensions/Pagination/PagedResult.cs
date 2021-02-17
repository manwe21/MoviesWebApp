using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Data.QueryExtensions.Pagination
{
    public class PagedResult<T>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int AllRows { get; set; }
        public int PagesCount { get; set; }
        public List<T> Results { get; set; }

    }

}
