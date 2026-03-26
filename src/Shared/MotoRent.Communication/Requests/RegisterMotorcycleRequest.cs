namespace MotoRent.Communication.Requests;

public sealed record RegisterMotorcycleRequest(
    string LicensePlate,
    string Vin,
    string Model,
    string Brand,
    int Year);
