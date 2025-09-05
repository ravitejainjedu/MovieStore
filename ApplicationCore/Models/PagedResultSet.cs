using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    /*
    public sealed class PagedResultSet<T>
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public IReadOnlyList<T> Data { get; }

        public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPrevious => PageIndex > 1;
        public bool HasNext => PageIndex < TotalPages;

        
        public PagedResultSet(int pageIndex, int pageSize, int totalCount, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex < 1 ? 1 : pageIndex;
            PageSize = pageSize < 1 ? 30 : pageSize;
            TotalCount = totalCount < 0 ? 0 : totalCount;
            Data = data ?? Array.Empty<T>();
        }
    }
    */

    // Non-generic base so any view/partial (e.g., _Pager) can use it
    public abstract class PagedResultSetBase
    {
        public int Page { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPrevious => Page > 1;
        public bool HasNext => Page < TotalPages;

        protected PagedResultSetBase(int page, int pageSize, int totalCount)
        {
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }

    // Generic wrapper carrying the page of data
    public sealed class PagedResultSet<T> : PagedResultSetBase
    {
        public IReadOnlyList<T> Data { get; }

        public PagedResultSet(int page, int pageSize, int totalCount, IReadOnlyList<T> data)
            : base(page, pageSize, totalCount)
        {
            Data = data ?? Array.Empty<T>();
        }
    }

}
