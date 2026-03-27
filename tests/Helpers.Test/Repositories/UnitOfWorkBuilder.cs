using Moq;
using MotoRent.Domain.Repositories;

namespace Helpers.Test.Repositories;

public static class UnitOfWorkBuilder
{
    public static IUnitOfWork Build()
    {
        var mock = new Mock<IUnitOfWork>();

        return mock.Object;
    }
}
