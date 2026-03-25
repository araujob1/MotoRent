namespace MotoRent.Domain.Repositories;

public interface IUnitOfWork
{
    public Task Commit();
}
