using Bogus;
using MotoRent.Application.Validators;
using MotoRent.Communication.Requests;

namespace Helpers.Test.Requests;

public static class RegisterMotorcycleRequestBuilder
{
    public static RegisterMotorcycleRequest Build()
    {
        return new Faker<RegisterMotorcycleRequest>()
            .CustomInstantiator(f => new RegisterMotorcycleRequest(
                LicensePlate: f.Random.AlphaNumeric(length: 7),
                Vin: f.Vehicle.Vin(),
                Model: f.Vehicle.Model(),
                Brand: f.Vehicle.Manufacturer(),
                Year: f.Random.Int(min: YearConstraints.MinYear, max: YearConstraints.MaxYear)))
            .Generate();
    }
}
