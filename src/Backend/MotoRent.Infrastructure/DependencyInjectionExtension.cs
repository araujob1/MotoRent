using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotoRent.Domain.Repositories;
using MotoRent.Domain.Repositories.Motorcycle;
using MotoRent.Infrastructure.DataAccess;
using MotoRent.Infrastructure.DataAccess.Repositories;
using MotoRent.Infrastructure.Extensions;

namespace MotoRent.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);

        AddDbContext(services, configuration);
        AddFluentMigrator(services, configuration);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetDbConnectionString();

        services.AddDbContext<MotoRentDbContext>(x =>
        {
            x.UseNpgsql(connectionString);
        });
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetDbConnectionString();

        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("MotoRent.Infrastructure")).For.All());
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IMotorcycleReadOnlyRepository, MotorcycleRepository>();
        services.AddScoped<IMotorcycleWriteOnlyRepository, MotorcycleRepository>();
    }
}
