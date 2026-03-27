using FluentValidation;
using MotoRent.Application.Validators;
using MotoRent.Communication.Requests;
using MotoRent.Exceptions;

namespace MotoRent.Application.UseCases.Motorcycle.Register;

public class RegisterMotorcycleValidator : AbstractValidator<RegisterMotorcycleRequest>
{
    public RegisterMotorcycleValidator()
    {
        RuleFor(x => x.LicensePlate).NotEmpty().WithMessage(ErrorMessages.EMPTY_LICENSE_PLATE);
        RuleFor(x => x.Vin).NotEmpty().WithMessage(ErrorMessages.EMPTY_VIN);
        RuleFor(x => x.Model).NotEmpty().WithMessage(ErrorMessages.EMPTY_MODEL);
        RuleFor(x => x.Brand).NotEmpty().WithMessage(ErrorMessages.EMPTY_BRAND);
        RuleFor(x => x.Year).ValidYear();
    }
}
