using MotoRent.Domain.Repositories;

namespace MotoRent.Infrastructure.DataAccess;

public class UnitOfWork(MotoRentDbContext dbContext) : IUnitOfWork
{
    private readonly MotoRentDbContext _dbContext = dbContext;

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
