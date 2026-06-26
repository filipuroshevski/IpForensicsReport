using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Paging
{
    public class PagingModel
    {
        public int TotalPages { get; set; }
        public int TotalRows { get; set; }
        public int PageSize { get; set; }
        public int CurrentPageNumber { get; set; }
    }
}
