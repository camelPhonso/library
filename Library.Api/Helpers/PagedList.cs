using Microsoft.EntityFrameworkCore;

namespace Library.Api.Helpers;

public class PagedList<T>
{
    public List<T> Items { get; }
    public int PageIndex { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public bool HasNextPage => (PageIndex * PageSize) < TotalCount;
    public bool HasPreviousPage => PageIndex > 1;

    private PagedList(List<T> items, int pageIndex, int pageSize, int totalCount)
    {
        Items = items;
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public static async Task<PagedList<T>> CreateAsync(
        IQueryable<T> query,
        int pageIndex,
        int pageSize
    )
    {
        var finalCount = await query.CountAsync();

        var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        return new(items, pageIndex, pageSize, finalCount);
    }
}
