using Offices.Application.DTOs.Pagination; 

namespace Offices.Application.Abstractions;

public interface IRepository<T>
{
    Task AddAsync(T entity);
    Task<T?> GetByIdAsync(Guid id);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<PagedResult<T>> GetAllAsync(PageParams pageParams);
}