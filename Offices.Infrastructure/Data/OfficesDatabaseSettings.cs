namespace Offices.Infrastructure.Data;

public class OfficesDatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string OfficesCollectionName { get; set; } = string.Empty;
}
