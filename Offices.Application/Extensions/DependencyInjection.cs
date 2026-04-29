using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Offices.Application.Abstractions;
using Offices.Application.Services;

namespace Offices.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer(); 
        services.AddSwaggerGen();
        services.AddScoped<IOfficeService, OfficeService>();

        services.AddValidatorsFromAssemblyContaining<IOfficeService>();

        return services;
    }
}