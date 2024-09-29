using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;

namespace ProfileManager.Paginate
{
    public class PagedList<T> : List<T>
    {
 

        public PagedList(IEnumerable<T> currentPage, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(currentPage);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static async Task<Paginate<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber-1) * pageSize).Take(pageSize).ToListAsync();
            //var items = await source.Skip((pageNumber) * pageSize).Take(pageSize).ToListAsync();
 
            var result = new PagedList<T>(items, count, pageNumber, pageSize);
            var resPag = new Paginate<T>
            {
                Data = result,
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
            };
            return resPag;
        }
    }

    //public class Paginate<T>
    //{
    //    public int CurrentPage { get; set; }
    //    public int TotalPages { get; set; }
    //    public int PageSize { get; set; }
    //    public int TotalCount { get; set; }
    //    public List<T>? Data { get; set; }
    //}

}
