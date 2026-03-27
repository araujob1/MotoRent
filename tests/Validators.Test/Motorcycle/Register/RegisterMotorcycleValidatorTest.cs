using Helpers.Test.ClassData;
using Helpers.Test.Requests;
using MotoRent.Application.UseCases.Motorcycle.Register;
using MotoRent.Application.Validators;
using MotoRent.Exceptions;
using Shouldly;

namespace Validators.Test.Motorcycle.Register;

public class RegisterMotorcycleValidatorTest
{
    private readonly RegisterMotorcycleValidator _validator = new();

    [Fact]
    public void Success()
    {
        var request = RegisterMotorcycleRequestBuilder.Build();

        var result = _validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [InlineData(nameof(YearConstraints.MinYear))]
    [InlineData(nameof(YearConstraints.MaxYear))]
    public void Success_Year_At_Boundary(string boundaryName)
    {
        var year = boundaryName == nameof(YearConstraints.MinYear)
            ? YearConstraints.MinYear
            : YearConstraints.MaxYear;

        var request = RegisterMotorcycleRequestBuilder.Build() with { Year = year };

        var result = _validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [ClassData(typeof(EmptyOrWhitespaceStringData))]
    public void Error_Empty_LicensePlate(string licensePlate)
    {
        var request = RegisterMotorcycleRequestBuilder.Build() with
        {
            LicensePlate = licensePlate
        };

        var result = _validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(e => e.ErrorMessage.Equals(ErrorMessages.EMPTY_LICENSE_PLATE));
        });
    }

    [Theory]
    [ClassData(typeof(EmptyOrWhitespaceStringData))]
    public void Error_Empty_Vin(string vin)
    {
        var request = RegisterMotorcycleRequestBuilder.Build() with
        {
            Vin = vin
        };

        var result = _validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(e => e.ErrorMessage.Equals(ErrorMessages.EMPTY_VIN));
        });
    }

    [Theory]
    [ClassData(typeof(EmptyOrWhitespaceStringData))]
    public void Error_Empty_Model(string model)
    {
        var request = RegisterMotorcycleRequestBuilder.Build() with
        {
            Model = model
        };

        var result = _validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(e => e.ErrorMessage.Equals(ErrorMessages.EMPTY_MODEL));
        });
    }

    [Theory]
    [ClassData(typeof(EmptyOrWhitespaceStringData))]
    public void Error_Empty_Brand(string brand)
    {
        var request = RegisterMotorcycleRequestBuilder.Build() with
        {
            Brand = brand
        };

        var result = _validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(e => e.ErrorMessage.Equals(ErrorMessages.EMPTY_BRAND));
        });
    }

    [Theory]
    [ClassData(typeof(InvalidYearData))]
    public void Error_Invalid_Year(int year)
    {
        var request = RegisterMotorcycleRequestBuilder.Build() with
        {
            Year = year
        };

        var result = _validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(e => e.ErrorMessage.Equals(ErrorMessages.INVALID_YEAR));
        });
    }
}

