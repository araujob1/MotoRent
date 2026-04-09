using MotoRent.Application.Mappings;
using MotoRent.Communication.Requests;
using MotoRent.Communication.Responses;
using MotoRent.Domain.Dtos;
using MotoRent.Domain.Pagination;
using MotoRent.Domain.Repositories.Motorcycle;
using MotoRent.Exceptions;

namespace MotoRent.Application.UseCases.Motorcycle.Filter;

public class FilterMotorcycleUseCase(IMotorcycleReadOnlyRepository motorcycleReadOnlyRepository) : IFilterMotorcycleUseCase
{
    private readonly IMotorcycleReadOnlyRepository _motorcycleReadOnlyRepository = motorcycleReadOnlyRepository;

    public async Task<Result<PagedResponse<MotorcycleResponse>>> Execute(FilterMotorcycleRequest request)
    {
        var errors = new FilterMotorcycleValidator()
            .Validate(request)
            .Errors
            .Select(error => error.ErrorMessage)
            .ToList();

        if (errors.Count > 0)
            return Result<PagedResponse<MotorcycleResponse>>.Failure(MotoRentError.Validation(errors));

        var filter = new MotorcycleFilterDto(
            request.LicensePlate,
            request.Vin,
            request.Year,
            new PageRequest(request.PageNumber, request.PageSize));

        var motorcycles = await _motorcycleReadOnlyRepository.Filter(filter);

        return Result<PagedResponse<MotorcycleResponse>>.Success(motorcycles.ToResponse());
    }
}
