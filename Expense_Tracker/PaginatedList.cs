using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int PageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            AddRange(items);
        }
        public bool HasPreviousPage
        {
            get
            {
                return PageIndex > 1;
            }
        }
        public bool HasNextPage
        {
            get
            {
                return PageIndex < TotalPages;
            }
        }
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> values, int pageIndex, int pageSize)
        {
            var count = await values.CountAsync();
            var items = await values.Skip((pageIndex - 1) * pageSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
