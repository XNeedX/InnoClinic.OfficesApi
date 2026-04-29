using Offices.Application;
using Offices.Infrastructure;
using Offices.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<OfficesDatabaseSettings>(builder.Configuration.GetSection("OfficeDatabaseSettings"));
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseSwagger(); 
app.UseSwaggerUI(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
