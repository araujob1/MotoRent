using Npgsql;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Testcontainers.PostgreSql;

namespace Integration.Test;

public sealed class CustomWebApplicationFactory(string connectionString) : WebApplicationFactory<Program>
{
    private static readonly SemaphoreSlim ContainerLock = new(1, 1);
    private static readonly PostgreSqlContainer PostgreSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private readonly string _connectionString = connectionString;

    public string ConnectionString => _connectionString;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("ConnectionStrings:DefaultConnection", _connectionString);

        builder.ConfigureAppConfiguration((_, configBuilder) =>
        {
            configBuilder.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnection"] = _connectionString
            });
        });
    }

    public static async Task<string> CreateConnectionStringAsync(string databaseName)
    {
        await EnsureContainerStartedAsync();

        var connectionBuilder = new NpgsqlConnectionStringBuilder(PostgreSqlContainer.GetConnectionString())
        {
            Database = databaseName
        };

        return connectionBuilder.ConnectionString;
    }

    private static async Task EnsureContainerStartedAsync()
    {
        if (PostgreSqlContainer.State == TestcontainersStates.Running)
            return;

        await ContainerLock.WaitAsync();

        try
        {
            if (PostgreSqlContainer.State != TestcontainersStates.Running)
                await PostgreSqlContainer.StartAsync();
        }
        finally
        {
            ContainerLock.Release();
        }
    }
}
