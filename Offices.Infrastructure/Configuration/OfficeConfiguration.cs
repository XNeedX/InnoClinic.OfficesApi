using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Offices.Domain.Models;

namespace Offices.Infrastructure.Configuration;

public static class OfficeConfiguration
{
    public static void Configure()
    {
        if (!BsonClassMap.IsClassMapRegistered(typeof(Office)))
        {
            BsonClassMap.RegisterClassMap<Office>(cm =>
            {
                cm.AutoMap();

                cm.MapIdProperty(x => x.Id)
                .SetSerializer(new GuidSerializer(BsonType.String));

                cm.UnmapMember(x => x.FullAddress);
            });

        }
    }
}
