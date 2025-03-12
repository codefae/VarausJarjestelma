using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace src.Modules.ReservationModule.Features.PatchReservationStartAndEndTimes;

public class PatchReservationStartAndEndTimesEndpoint : Endpoint
<
    PatchReservationStartAndEndTimesRequest,
    Results<Ok, NotFound, ProblemHttpResult>
>
{
    public override void Configure()
    {
        Patch("/rooms/{RoomId}/info");
        Validator<PatchReservationStartAndEndTimesValidator>();
        AllowAnonymous();
    }

    public override Task HandleAsync(PatchReservationStartAndEndTimesRequest req, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}