using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace Offices.Infrastructure.Data;

public class MongoContext
{
    public MongoClient Client { get; }
    public IMongoDatabase Database { get; }

    public MongoContext(IOptions<OfficesDatabaseSettings> settings)
    {
        Client = new MongoClient(settings.Value.ConnectionString);
        Database = Client.GetDatabase(settings.Value.DatabaseName);
    }
}