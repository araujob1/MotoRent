using Microsoft.EntityFrameworkCore;
using MotoRent.Domain.Entities;

namespace MotoRent.Infrastructure.DataAccess;

public class MotoRentDbContext(DbContextOptions<MotoRentDbContext> options) : DbContext(options)
{
    public DbSet<Motorcycle> Motorcycles { get; set; } = null!;

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MotoRentDbContext).Assembly);
    }
}
