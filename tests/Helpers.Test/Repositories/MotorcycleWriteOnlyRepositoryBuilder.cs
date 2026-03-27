using Moq;
using MotoRent.Domain.Repositories.Motorcycle;

namespace Helpers.Test.Repositories;

public static class MotorcycleWriteOnlyRepositoryBuilder
{
    public static IMotorcycleWriteOnlyRepository Build()
    {
        var mock = new Mock<IMotorcycleWriteOnlyRepository>();

        return mock.Object;
    }
}
