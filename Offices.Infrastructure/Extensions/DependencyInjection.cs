using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Offices.Application.Abstractions;
using Offices.Domain.Models;
using Offices.Infrastructure.Configuration;
using Offices.Infrastructure.Data;
using Offices.Infrastructure.Repositories;

namespace Offices.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        OfficeConfiguration.Configure();

        services.AddSingleton<MongoContext>();
        services.AddScoped<IRepository<Office>, OfficeRepository>();

        return services;
    }
}