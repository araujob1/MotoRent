using System.Net;
using Helpers.Test.Repositories;
using Helpers.Test.Requests;
using MotoRent.Application.UseCases.Motorcycle.Register;
using MotoRent.Domain.ValueObjects;
using MotoRent.Exceptions;
using Shouldly;

namespace UseCases.Test.Motorcycle.Register;

public class RegisterMotorcycleUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RegisterMotorcycleRequestBuilder.Build();

        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public async Task Error_Already_Exists_LicensePlate()
    {
        var request = RegisterMotorcycleRequestBuilder.Build();

        var useCase = CreateUseCase(licensePlate: new LicensePlate(request.LicensePlate));

        var result = await useCase.Execute(request);

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldNotBeNull();
        result.Error.Code.ShouldBe("validation_error");
        result.Error.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        result.Error.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(ErrorMessages.ALREADY_EXISTS_LICENSE_PLATE);
        });
    }

    [Fact]
    public async Task Error_Already_Exists_Vin()
    {
        var request = RegisterMotorcycleRequestBuilder.Build();

        var useCase = CreateUseCase(vin: new Vin(request.Vin));

        var result = await useCase.Execute(request);

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldNotBeNull();
        result.Error.Code.ShouldBe("validation_error");
        result.Error.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        result.Error.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(ErrorMessages.ALREADY_EXISTS_VIN);
        });
    }

    [Fact]
    public async Task Error_Already_Exists_LicensePlate_And_Vin()
    {
        var request = RegisterMotorcycleRequestBuilder.Build();

        var useCase = CreateUseCase(
            licensePlate: new LicensePlate(request.LicensePlate),
            vin: new Vin(request.Vin));

        var result = await useCase.Execute(request);

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldNotBeNull();
        result.Error.Code.ShouldBe("validation_error");
        result.Error.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        result.Error.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(2);
            errors.ShouldContain(ErrorMessages.ALREADY_EXISTS_LICENSE_PLATE);
            errors.ShouldContain(ErrorMessages.ALREADY_EXISTS_VIN);
        });
    }

    [Fact]
    public async Task Error_When_LicensePlate_Is_Null()
    {
        var request = RegisterMotorcycleRequestBuilder.Build() with
        {
            LicensePlate = null!
        };

        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldNotBeNull();
        result.Error.Code.ShouldBe("validation_error");
        result.Error.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        result.Error.Errors.ShouldContain(ErrorMessages.EMPTY_LICENSE_PLATE);
    }

    [Fact]
    public async Task Error_When_Vin_Is_Null()
    {
        var request = RegisterMotorcycleRequestBuilder.Build() with
        {
            Vin = null!
        };

        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldNotBeNull();
        result.Error.Code.ShouldBe("validation_error");
        result.Error.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        result.Error.Errors.ShouldContain(ErrorMessages.EMPTY_VIN);
    }

    private static RegisterMotorcycleUseCase CreateUseCase(LicensePlate? licensePlate = null, Vin? vin = null)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var readOnly = new MotorcycleReadOnlyRepositoryBuilder();
        var writeOnly = MotorcycleWriteOnlyRepositoryBuilder.Build();

        if (licensePlate is not null)
            readOnly.ExistActiveMotorcycleWithLicensePlate(licensePlate);

        if (vin is not null)
            readOnly.ExistActiveMotorcycleWithVin(vin);

        return new RegisterMotorcycleUseCase(unitOfWork, readOnly.Build(), writeOnly);
    }
}
