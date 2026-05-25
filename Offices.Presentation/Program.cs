using Offices.Application;
using Offices.Infrastructure;
using Offices.Infrastructure.Data;
using Offices.Presentation.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddSerilogLogging(builder.Configuration);

builder.Services.Configure<OfficesDatabaseSettings>(builder.Configuration.GetSection("OfficeDatabaseSettings"));
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.UseMiddlewarePipeline();

app.Run();
