using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace src.Modules.ReservationModule.Features.PatchReservationStartAndEndTimes;

public class PatchReservationTimeEndpoint : Endpoint
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