using System.Globalization;
using MotoRent.Api.Endpoints;
using MotoRent.Api.Filters;
using MotoRent.Application;
using MotoRent.Infrastructure;
using MotoRent.Infrastructure.Extensions;
using MotoRent.Infrastructure.Migrations;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandler<MotoRentExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultureNames = CultureInfo.GetCultures(CultureTypes.AllCultures)
        .Where(c => !string.IsNullOrEmpty(c.Name))
        .Select(c => c.Name)
        .ToArray();

    options.SetDefaultCulture("en")
        .AddSupportedCultures(supportedCultureNames)
        .AddSupportedUICultures(supportedCultureNames);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseRequestLocalization();

app.UseExceptionHandler();

app.MapMotorcycleEndpoints();

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

public partial class Program
{
    protected Program() { }
}
