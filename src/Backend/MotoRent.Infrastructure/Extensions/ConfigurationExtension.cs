using Microsoft.Extensions.Configuration;

namespace MotoRent.Infrastructure.Extensions;

public static class ConfigurationExtension
{
    public static string GetDbConnectionString(this IConfiguration configuration) =>
        configuration.GetConnectionString("DefaultConnection")!;
}
