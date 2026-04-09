using MotoRent.Application.Mappings;
using MotoRent.Communication.Requests;
using MotoRent.Communication.Responses;
using MotoRent.Domain.Extensions;
using MotoRent.Domain.Repositories;
using MotoRent.Domain.Repositories.Motorcycle;
using MotoRent.Domain.ValueObjects;
using MotoRent.Exceptions;

namespace MotoRent.Application.UseCases.Motorcycle.Register;

public class RegisterMotorcycleUseCase(
    IUnitOfWork unitOfWork,
    IMotorcycleReadOnlyRepository motorcycleReadOnlyRepository,
    IMotorcycleWriteOnlyRepository motorcycleWriteOnlyRepository) : IRegisterMotorcycleUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMotorcycleReadOnlyRepository _motorcycleReadOnlyRepository = motorcycleReadOnlyRepository;
    private readonly IMotorcycleWriteOnlyRepository _motorcycleWriteOnlyRepository = motorcycleWriteOnlyRepository;

    public async Task<Result<RegisterMotorcycleResponse>> Execute(RegisterMotorcycleRequest request)
    {
        var errors = await Validate(request);

        if (errors.Count > 0)
            return Result<RegisterMotorcycleResponse>.Failure(MotoRentError.Validation(errors));

        var motorcycle = request.ToEntity();

        await _motorcycleWriteOnlyRepository.Add(motorcycle);
        await _unitOfWork.Commit();

        return Result<RegisterMotorcycleResponse>.Success(new RegisterMotorcycleResponse(motorcycle.Id));
    }

    private async Task<List<string>> Validate(RegisterMotorcycleRequest request)
    {
        var validationResult = new RegisterMotorcycleValidator().Validate(request);

        var errors = validationResult.Errors
            .Select(e => e.ErrorMessage)
            .ToList();

        if (request.LicensePlate.NotEmpty())
        {
            var alreadyExistsWithLicensePlate = await _motorcycleReadOnlyRepository
                .AlreadyExistsWithLicensePlate(new LicensePlate(request.LicensePlate));

            if (alreadyExistsWithLicensePlate.IsTrue())
                errors.Add(ErrorMessages.ALREADY_EXISTS_LICENSE_PLATE);
        }

        if (request.Vin.NotEmpty())
        {
            var alreadyExistsWithVin = await _motorcycleReadOnlyRepository
                .AlreadyExistsWithVin(new Vin(request.Vin));

            if (alreadyExistsWithVin.IsTrue())
                errors.Add(ErrorMessages.ALREADY_EXISTS_VIN);
        }

        return errors;
    }
}
