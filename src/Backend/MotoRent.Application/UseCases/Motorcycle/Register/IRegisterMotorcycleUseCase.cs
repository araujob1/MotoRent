using MotoRent.Communication.Requests;
using MotoRent.Communication.Responses;
using MotoRent.Exceptions;

namespace MotoRent.Application.UseCases.Motorcycle.Register;

public interface IRegisterMotorcycleUseCase
{
    Task<Result<RegisterMotorcycleResponse>> Execute(RegisterMotorcycleRequest request);
}
