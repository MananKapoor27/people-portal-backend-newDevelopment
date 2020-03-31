using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PeoplePortalDomainLayer.HelperModels.PaginationModels
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }

        public int TotalCount { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public bool HasNextPage => CurrentPage < TotalPages;

        public bool HasPreviousPage => CurrentPage > 1;

        public PagedList(List<T> itemsList, int count, int pageSize, int pageNumber)
        {
            AddRange(itemsList);
            TotalCount = count;
            TotalPages = (int)(Math.Ceiling((double)TotalCount / pageSize));
            PageSize = pageSize;
            CurrentPage = pageNumber;
        }

        public static async Task<PagedList<T>> Create(IQueryable<T> itemSource, int pageNumber, int pageSize)
        {
            int count = itemSource.Count();
            var items = await itemSource.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageSize, pageNumber);
        }
    }
}
