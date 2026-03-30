using MotoRent.Application.UseCases.Motorcycle.Register;
using MotoRent.Communication.Requests;
using MotoRent.Communication.Responses;

namespace MotoRent.Api.Endpoints;

public static class MotorcycleEndpoints
{
    public static void MapMotorcycleEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/motorcycles")
            .WithTags("Motorcycles");

        group.MapPost("/", async (RegisterMotorcycleRequest request, IRegisterMotorcycleUseCase useCase) =>
        {
            var result = await useCase.Execute(request);

            return Results.Created(string.Empty, result);
        })
        .WithName("RegisterMotorcycle")
        .WithSummary("Register a new motorcycle")
        .WithDescription("Creates a new motorcycle in the system with the provided details. License plate and VIN must be unique across all active motorcycles.")
        .Produces<RegisterMotorcycleResponse>(StatusCodes.Status201Created)
        .Produces<ErrorResponse>(StatusCodes.Status400BadRequest);
    }
}
