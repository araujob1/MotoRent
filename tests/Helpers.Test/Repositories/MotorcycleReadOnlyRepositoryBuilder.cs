using Moq;
using MotoRent.Domain.Repositories.Motorcycle;
using MotoRent.Domain.ValueObjects;

namespace Helpers.Test.Repositories;

public class MotorcycleReadOnlyRepositoryBuilder
{
    private readonly Mock<IMotorcycleReadOnlyRepository> _repository;

    public MotorcycleReadOnlyRepositoryBuilder() => _repository = new Mock<IMotorcycleReadOnlyRepository>();

    public MotorcycleReadOnlyRepositoryBuilder ExistActiveMotorcycleWithLicensePlate(LicensePlate licensePlate)
    {
        _repository.Setup(x => x.AlreadyExistsWithLicensePlate(licensePlate)).ReturnsAsync(true);

        return this;
    }

    public MotorcycleReadOnlyRepositoryBuilder ExistActiveMotorcycleWithVin(Vin vin)
    {
        _repository.Setup(x => x.AlreadyExistsWithVin(vin)).ReturnsAsync(true);

        return this;
    }

    public IMotorcycleReadOnlyRepository Build() => _repository.Object;
}
