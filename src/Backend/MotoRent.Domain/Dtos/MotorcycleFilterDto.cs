using MotoRent.Domain.Pagination;

namespace MotoRent.Domain.Dtos;

public sealed record MotorcycleFilterDto(
    string? LicensePlate,
    string? Vin,
    int? Year,
    PageRequest Page);
