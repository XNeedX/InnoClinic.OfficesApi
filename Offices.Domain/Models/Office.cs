using MongoDB.Bson.Serialization.Attributes;

namespace Offices.Domain.Models;
public class Office
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id { get; set; }
    public string? PhotoPath { get; set; } 
    public string City { get; set; } = string.Empty; 
    public string Street { get; set; } = string.Empty; 
    public string HouseNumber { get; set; } = string.Empty; 
    public string? OfficeNumber { get; set; } 
    public string RegistryPhoneNumber { get; set; } = string.Empty; 
    public OfficeStatus Status { get; set; }
    public string FullAddress => $"{City}, {Street}, {HouseNumber}, {OfficeNumber}";
}