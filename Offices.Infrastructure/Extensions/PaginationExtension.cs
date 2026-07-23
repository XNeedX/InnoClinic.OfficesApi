using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Offices.Application.DTOs.Pagination;

namespace Offices.Infrastructure.Extensions;

public static class PaginationExtension
{
    public static async Task<PagedResult<T>> ToPagedAsync<T>(this IQueryable<T> query, PageParams pageParams)
    {
        var count = await query.CountAsync();

        if (count == 0)
            return new PagedResult<T>(Array.Empty<T>(), 0);

        var skip = (pageParams.Page - 1) * pageParams.PageSize;

        var result = await query.Skip(skip)
                                .Take(pageParams.PageSize)
                                .ToListAsync();

        return new PagedResult<T>(result, count);
    }
}