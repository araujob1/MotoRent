using MotoRent.Communication.Requests;
using MotoRent.Communication.Responses;
using MotoRent.Exceptions;

namespace MotoRent.Application.UseCases.Motorcycle.Filter;

public interface IFilterMotorcycleUseCase
{
    Task<Result<PagedResponse<MotorcycleResponse>>> Execute(FilterMotorcycleRequest request);
}
