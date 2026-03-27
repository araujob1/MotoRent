using MotoRent.Api.Filters;
using MotoRent.Application;
using MotoRent.Infrastructure;
using MotoRent.Infrastructure.Extensions;
using MotoRent.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandler<MotoRentExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

MigrateDatabase();

await app.RunAsync();

void MigrateDatabase()
{
    var connectionString = builder
        .Configuration
        .GetDbConnectionString();

    var serviceScope = app
        .Services
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope();

    DatabaseMigration.Migrate(connectionString, serviceScope.ServiceProvider);
}

