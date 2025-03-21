using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Features.GetAvailableRooms;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Features.DeleteReservation;

public class DeleteReservationEndpoint(IReservationRepository reservationRepository) : EndpointWithoutRequest
<
    Results<Ok, NotFound, ProblemHttpResult>
>
{
    public override void Configure()
    {
        Delete("/reservation/{id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var result = await reservationRepository.DeleteAsync(id, ct);
        if (!result)
        {
            await SendAsync(TypedResults.NotFound());
            return;
        }
        await SendAsync(TypedResults.Ok());
    }
}

