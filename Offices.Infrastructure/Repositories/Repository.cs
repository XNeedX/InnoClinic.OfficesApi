using MongoDB.Driver;
using Offices.Application.Abstractions;
using Offices.Domain.Models;
using Offices.Infrastructure.Data;
using Microsoft.Extensions.Options;

namespace Offices.Infrastructure.Repositories;

public class OfficeRepository : IRepository<Office>
{
    private readonly MongoContext _context;
    private readonly IMongoCollection<Office> _collection;

    public OfficeRepository(MongoContext context, IOptions<OfficesDatabaseSettings> settings)
    {
        _context = context;
        _collection = _context.Database.GetCollection<Office>(settings.Value.OfficesCollectionName);
    }

    public async Task AddAsync(Office entity)
    {
        if (_context.Session == null)
        {
            _context.Session = await _context.Client.StartSessionAsync();
            _context.Session.StartTransaction();
        }

        await _collection.InsertOneAsync(_context.Session, entity);
    }

    public async Task<Office?> GetByIdAsync(Guid id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Office entity)
    {
        if (_context.Session == null)
        {
            _context.Session = await _context.Client.StartSessionAsync();
            _context.Session.StartTransaction();
        }

        await _collection.ReplaceOneAsync(
            _context.Session,
            x => x.Id == entity.Id,
            entity);
    }

    public async Task DeleteAsync(Office entity)
    {
        if (_context.Session == null)
        {
            _context.Session = await _context.Client.StartSessionAsync();
            _context.Session.StartTransaction();
        }

        await _collection.DeleteOneAsync(
            _context.Session,
            x => x.Id == entity.Id);
    }

    public async Task<IEnumerable<Office>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
}