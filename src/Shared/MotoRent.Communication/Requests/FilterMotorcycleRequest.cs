namespace MotoRent.Communication.Requests;

public sealed record FilterMotorcycleRequest : PagedRequest
{
    public string? LicensePlate { get; init; }
    public string? Vin { get; init; }
    public int? Year { get; init; }
}
