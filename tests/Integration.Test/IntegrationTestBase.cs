using System.Net.Http.Json;

namespace Integration.Test;

public abstract class IntegrationTestBase : IAsyncLifetime
{
    private CustomWebApplicationFactory? _factory;
    private HttpClient? _httpClient;

    protected HttpClient HttpClient => _httpClient ?? throw new InvalidOperationException("The HTTP client was not initialized.");

    public async ValueTask InitializeAsync()
    {
        var databaseName = $"motorent_test_{Guid.NewGuid():N}";
        var connectionString = await CustomWebApplicationFactory.CreateConnectionStringAsync(databaseName);

        _factory = new CustomWebApplicationFactory(connectionString);
        _httpClient = _factory.CreateClient();
    }

    public async ValueTask DisposeAsync()
    {
        _httpClient?.Dispose();

        if (_factory is not null)
            await _factory.DisposeAsync();

        GC.SuppressFinalize(this);
    }

    protected async Task<HttpResponseMessage> DoPost(
        string method,
        object request,
        string culture = "en",
        CancellationToken cancellationToken = default)
    {
        ChangeRequestCulture(culture);

        return await HttpClient.PostAsJsonAsync(method, request, cancellationToken);
    }

    private void ChangeRequestCulture(string culture)
    {
        if (HttpClient.DefaultRequestHeaders.Contains("Accept-Language"))
            HttpClient.DefaultRequestHeaders.Remove("Accept-Language");

        HttpClient.DefaultRequestHeaders.Add("Accept-Language", culture);
    }
}
