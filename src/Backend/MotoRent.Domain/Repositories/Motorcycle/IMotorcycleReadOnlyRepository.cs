using MotoRent.Domain.ValueObjects;

namespace MotoRent.Domain.Repositories.Motorcycle;

public interface IMotorcycleReadOnlyRepository
{
    Task<bool> AlreadyExistsWithLicensePlate(LicensePlate licensePlate);
    Task<bool> AlreadyExistsWithVin(Vin vin);
}
