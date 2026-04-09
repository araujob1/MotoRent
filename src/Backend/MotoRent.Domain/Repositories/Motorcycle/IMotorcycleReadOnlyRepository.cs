using MotoRent.Domain.Dtos;
using MotoRent.Domain.ValueObjects;
using MotoRent.Domain.Pagination;

namespace MotoRent.Domain.Repositories.Motorcycle;

public interface IMotorcycleReadOnlyRepository
{
    Task<bool> AlreadyExistsWithLicensePlate(LicensePlate licensePlate);
    Task<bool> AlreadyExistsWithVin(Vin vin);
    Task<PagedResult<Entities.Motorcycle>> Filter(MotorcycleFilterDto filter);
}
