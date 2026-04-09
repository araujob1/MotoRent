using Microsoft.EntityFrameworkCore;
using MotoRent.Domain.Dtos;
using MotoRent.Domain.Entities;
using MotoRent.Domain.Extensions;
using MotoRent.Domain.Pagination;
using MotoRent.Domain.Repositories.Motorcycle;
using MotoRent.Domain.ValueObjects;
using MotoRent.Infrastructure.DataAccess.Extensions;

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

    public async Task<PagedResult<Motorcycle>> Filter(MotorcycleFilterDto filter)
    {
        var query = _dbContext.Motorcycles
            .AsNoTracking()
            .Where(m => m.IsActive);

        if (filter.LicensePlate.NotEmpty())
        {
            var licensePlate = new LicensePlate(filter.LicensePlate);
            query = query.Where(m => m.LicensePlate == licensePlate);
        }

        if (filter.Vin.NotEmpty())
        {
            var vin = new Vin(filter.Vin);
            query = query.Where(m => m.Vin == vin);
        }

        if (filter.Year.HasValue)
            query = query.Where(m => m.Year == filter.Year.Value);

        return await query
            .OrderByDescending(m => m.CreatedAt)
            .ToPagedResultAsync(filter.Page);
    }
}
