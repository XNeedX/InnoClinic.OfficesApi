using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Offices.Application.Abstractions;
using Offices.Application.DTOs.Pagination;
using Offices.Domain.Models;
using Offices.Infrastructure.Data;
using Offices.Infrastructure.Extensions;

namespace Offices.Infrastructure.Repositories;

public class OfficeRepository : IRepository<Office>
{
    private readonly IMongoCollection<Office> _collection;

    public OfficeRepository(MongoContext context, IOptions<OfficesDatabaseSettings> settings)
    {
        _collection = context.Database.GetCollection<Office>(settings.Value.OfficesCollectionName);
    }

    public async Task AddAsync(Office entity) => await _collection.InsertOneAsync(entity);

    public async Task<Office?> GetByIdAsync(Guid id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task UpdateAsync(Office entity) => await _collection.ReplaceOneAsync(x => x.Id == entity.Id,entity);

    public async Task DeleteAsync(Office entity) => await _collection.DeleteOneAsync(x => x.Id == entity.Id);

    public async Task<PagedResult<Office>> GetAllAsync(PageParams pageParams) => await _collection.AsQueryable().ToPagedAsync(pageParams);

}