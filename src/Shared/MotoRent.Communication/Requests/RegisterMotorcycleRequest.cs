namespace MotoRent.Communication.Requests;

public record RegisterMotorcycleRequest(
    string LicensePlate,
    string Vin,
    string Model,
    string Brand,
    int Year);
