namespace MotoRent.Communication.Responses;

public sealed record MotorcycleResponse(
    Guid Id,
    string LicensePlate,
    string Vin,
    string Model,
    string Brand,
    int Year);
