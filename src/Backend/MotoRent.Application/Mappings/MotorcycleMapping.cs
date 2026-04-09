using MotoRent.Communication.Requests;
using MotoRent.Domain.Entities;
using MotoRent.Domain.Pagination;
using MotoRent.Communication.Responses;
using MotoRent.Domain.ValueObjects;

namespace MotoRent.Application.Mappings;

public static class MotorcycleMapping
{
    public static Motorcycle ToEntity(this RegisterMotorcycleRequest request) =>
        new()
        {
            LicensePlate = new LicensePlate(request.LicensePlate),
            Vin = new Vin(request.Vin),
            Model = request.Model,
            Brand = request.Brand,
            Year = request.Year
        };

    public static MotorcycleResponse ToResponse(this Motorcycle motorcycle) =>
        new(
            motorcycle.Id,
            motorcycle.LicensePlate.Value,
            motorcycle.Vin.Value,
            motorcycle.Model,
            motorcycle.Brand,
            motorcycle.Year);

    public static PagedResponse<MotorcycleResponse> ToResponse(this PagedResult<Motorcycle> pagedMotorcycles) =>
        new(
            [.. pagedMotorcycles.Items.Select(ToResponse)],
            pagedMotorcycles.PageNumber,
            pagedMotorcycles.PageSize,
            pagedMotorcycles.TotalCount);
}
