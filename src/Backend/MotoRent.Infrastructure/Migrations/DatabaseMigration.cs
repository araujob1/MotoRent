using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using MotoRent.Domain.Extensions;
using Npgsql;

namespace MotoRent.Infrastructure.Migrations;

public static class DatabaseMigration
{
    public static void Migrate(string connectionString, IServiceProvider serviceProvider)
    {
        EnsureDatabaseCreated(connectionString);
        MigrationDatabase(serviceProvider);
    }

    private static void EnsureDatabaseCreated(string connectionString)
    {
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);
        var databaseName = connectionStringBuilder.Database;

        connectionStringBuilder.Database = "postgres";

        using var dbConnection = new NpgsqlConnection(connectionStringBuilder.ConnectionString);

        var databaseExists = dbConnection.ExecuteScalar<bool>(
            "SELECT EXISTS(SELECT 1 FROM pg_database WHERE datname = @name)",
            new { name = databaseName });

        if (databaseExists.IsFalse())
            dbConnection.Execute($"CREATE DATABASE {databaseName}");
    }

    private static void MigrationDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider
            .GetRequiredService<IMigrationRunner>();

        if (runner.HasMigrationsToApplyUp())
            runner.MigrateUp();
    }
}
