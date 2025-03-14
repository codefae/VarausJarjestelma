using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Features.GetAvailableRooms;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Features.DeleteReservation;

public class DeleteReservationEndpoint(IReservationRepository reservationRepository) : Endpoint
<
    DeleteReservationRequest,
    Results<Ok, NotFound, ProblemHttpResult>
>
{
    public override void Configure()
    {
        Delete("/reservation");
        Validator<DeleteReservationRequestValidator>();
        AllowAnonymous();
    }

    public override Task HandleAsync(DeleteReservationRequest req, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}