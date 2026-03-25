namespace MotoRent.Domain.Repositories.Motorcycle;

public interface IMotorcycleWriteOnlyRepository
{
    Task Add(Entities.Motorcycle motorcycle);
}
