namespace Offices.Application.DTOs.Pagination;
public record PageParams
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}