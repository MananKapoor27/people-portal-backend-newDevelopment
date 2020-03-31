using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.HelperModels.PaginationModels
{
    public class PagingParams
    {
        private int _pageSize = 5;

        //public int Rating { get; set; }

        public int PageNumber { get; set; } = 1;

        private const int maxPageSize = 25;

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value >= maxPageSize ? maxPageSize : value;
            }
        }
    }
}
