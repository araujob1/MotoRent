using MotoRent.Communication.Requests;
using MotoRent.Domain.Entities;
using MotoRent.Domain.ValueObjects;

namespace MotoRent.Application.Mappings;

public static class MotorcycleMapping
{
    public static Motorcycle ToEntity(this RegisterMotorcycleRequest request) =>
        new()
        {
            LicensePlate = new LicensePlate(request.LicensePlate),
            Vin = new Vin(request.Vin),
            Model = request.Model,
            Brand = request.Brand,
            Year = request.Year
        };
}
