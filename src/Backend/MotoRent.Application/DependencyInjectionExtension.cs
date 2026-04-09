using Microsoft.Extensions.DependencyInjection;
using MotoRent.Application.UseCases.Motorcycle.Filter;
using MotoRent.Application.UseCases.Motorcycle.Register;

namespace MotoRent.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IFilterMotorcycleUseCase, FilterMotorcycleUseCase>();
        services.AddScoped<IRegisterMotorcycleUseCase, RegisterMotorcycleUseCase>();
    }
}
