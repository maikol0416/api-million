using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Common;

namespace Infrastructure
{
    public class Paginator<T> where T : class, new()
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public Paginator(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<Paginate<T>> Paginate(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            await Task.CompletedTask;
            return new Paginate<T>()
            {
                Count = Convert.ToInt32(pageSize),
                Data = items,
                Page = pageIndex,
                RowsTotal = count,
                PagesTotal = (int)Math.Ceiling(count / (double)pageSize)
            };
        }
    }
}
