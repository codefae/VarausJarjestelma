using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Features.GetAvailableRooms;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Features.DeleteReservation;

public class DeleteReservationEndpoint(IReservationRepository reservationRepository) : EndpointWithoutRequest
<
Results<Ok, NotFound>>
{
    public override void Configure()
    {
        Delete("/reservation/{id}");
        AllowAnonymous();
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        
        throw new NotImplementedException();
    }
}