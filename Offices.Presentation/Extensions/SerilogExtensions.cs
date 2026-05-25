using Elastic.Serilog.Sinks;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Offices.Presentation.Extensions;

public static class SerilogExtensions
{
    public static void AddSerilogLogging(this IHostBuilder host, IConfiguration configuration)
    {
        host.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(configuration);

            var endpoint = configuration["ElasticCloud:Endpoint"];
            var username = configuration["ElasticCloud:Username"];
            var password = configuration["ElasticCloud:Password"];

            if (!string.IsNullOrEmpty(endpoint))
            {
                loggerConfiguration.WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri(endpoint))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = "innoclinic-officesapi-logs-{0:yyyy.MM.dd}",
                    ModifyConnectionSettings = x => x.BasicAuthentication(username, password)
                });
            }
        });
    }
}