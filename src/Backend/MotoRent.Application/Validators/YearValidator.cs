using FluentValidation;
using MotoRent.Exceptions;

namespace MotoRent.Application.Validators;

public static class YearConstraints
{
    public static readonly int MinYear = DateTime.UtcNow.Year - 2;
    public static readonly int MaxYear = DateTime.UtcNow.Year + 1;
}

public static class YearValidatorExtension
{
    public static IRuleBuilderOptions<T, int> ValidYear<T>(this IRuleBuilder<T, int> ruleBuilder)
    {
        return ruleBuilder
            .InclusiveBetween(YearConstraints.MinYear, YearConstraints.MaxYear)
            .WithMessage(ErrorMessages.INVALID_YEAR);
    }
}
