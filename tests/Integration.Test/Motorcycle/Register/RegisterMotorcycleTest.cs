using System.Globalization;
using System.Net;
using System.Text.Json;
using Helpers.Test.ClassData;
using Helpers.Test.Requests;
using MotoRent.Exceptions;
using Shouldly;

namespace Integration.Test.Motorcycle.Register;

public class RegisterMotorcycleTest : IntegrationTestBase
{
    private const string Method = "/api/motorcycles/";

    [Fact]
    public async Task Success()
    {
        var cancellationToken = TestContext.Current.CancellationToken;

        var request = RegisterMotorcycleRequestBuilder.Build();

        var response = await DoPost(
            method: Method,
            request: request,
            cancellationToken: cancellationToken);

        await using var body = await response.Content.ReadAsStreamAsync(cancellationToken);
        var responseJson = await JsonDocument.ParseAsync(body, cancellationToken: cancellationToken);

        var id = responseJson.RootElement
            .GetProperty("id")
            .GetGuid();

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        id.ShouldNotBe(Guid.Empty);
    }

    [Theory]
    [ClassData(typeof(SupportedCulturesData))]
    public async Task Error_Already_Exists_LicensePlate(string culture)
    {
        var cancellationToken = TestContext.Current.CancellationToken;

        var requestMotorcycle1 = RegisterMotorcycleRequestBuilder.Build();

        await DoPost(
            method: Method,
            request: requestMotorcycle1,
            culture: culture,
            cancellationToken: cancellationToken);

        var requestMotorcycle2 = RegisterMotorcycleRequestBuilder.Build() with
        {
            LicensePlate = requestMotorcycle1.LicensePlate
        };

        var response = await DoPost(
            method: Method,
            request: requestMotorcycle2,
            culture: culture,
            cancellationToken: cancellationToken);

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        var expectedErrorMessage = ErrorMessages
            .ResourceManager
            .GetString("ALREADY_EXISTS_LICENSE_PLATE", new CultureInfo(culture));

        expectedErrorMessage.ShouldNotBeNull();
        using var responseJson = JsonDocument.Parse(responseContent);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        responseJson.RootElement.GetProperty("code").GetString().ShouldBe("validation_error");
        responseJson.RootElement.GetProperty("statusCode").GetInt32().ShouldBe((int)HttpStatusCode.BadRequest);
        responseJson.RootElement.GetProperty("errors")[0].GetString().ShouldBe(expectedErrorMessage);
    }
}
