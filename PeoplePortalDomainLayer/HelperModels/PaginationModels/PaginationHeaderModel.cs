using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.HelperModels.PaginationModels
{
    public class PaginationHeaderModel
    {
        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }
    }
}
