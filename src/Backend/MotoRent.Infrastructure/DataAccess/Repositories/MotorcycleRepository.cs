using Microsoft.EntityFrameworkCore;
using MotoRent.Domain.Entities;
using MotoRent.Domain.Repositories.Motorcycle;
using MotoRent.Domain.ValueObjects;

namespace MotoRent.Infrastructure.DataAccess.Repositories;

public class MotorcycleRepository(MotoRentDbContext dbContext) : IMotorcycleReadOnlyRepository, IMotorcycleWriteOnlyRepository
{
    private readonly MotoRentDbContext _dbContext = dbContext;

    public async Task Add(Motorcycle motorcycle) =>
        await _dbContext.Motorcycles.AddAsync(motorcycle);

    public async Task<bool> AlreadyExistsWithLicensePlate(LicensePlate licensePlate) =>
        await _dbContext.Motorcycles.AnyAsync(m => m.IsActive && m.LicensePlate == licensePlate);

    public async Task<bool> AlreadyExistsWithVin(Vin vin) =>
        await _dbContext.Motorcycles.AnyAsync(m => m.IsActive && m.Vin == vin);
}
