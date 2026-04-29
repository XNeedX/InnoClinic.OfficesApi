namespace Offices.Application.DTOs.Pagination;
public record PagedResult<T>(IEnumerable<T> Items, long TotalCount);