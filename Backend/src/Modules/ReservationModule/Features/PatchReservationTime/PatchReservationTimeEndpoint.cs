using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Domain.DomainServices.Interfaces;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Features.PatchReservationTime;

public class PatchReservationTimeEndpoint(
    IRoomRepository roomRepository,
    IReservationRepository reservationRepository,
    IBookingDomainService bookingDomainService) : Endpoint
<
    PatchReservationTimeRequest,
    Results<Ok, NotFound, ProblemHttpResult>
>
{
    public override void Configure()
    {
        Patch("/rooms/{RoomId}/info");
        Validator<PatchReservationTimeValidator>();
        AllowAnonymous();
    }

    public override Task HandleAsync(PatchReservationTimeRequest req, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}