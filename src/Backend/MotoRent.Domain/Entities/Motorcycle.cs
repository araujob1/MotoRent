using MotoRent.Domain.ValueObjects;

namespace MotoRent.Domain.Entities;

public sealed class Motorcycle : EntityBase
{
    public LicensePlate LicensePlate { get; set; } = null!;
    public Vin Vin { get; set; } = null!;
    public string Model { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public int Year { get; set; }
}
