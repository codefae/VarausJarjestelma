using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Features.GetAvailableRooms;
using src.Modules.ReservationModule.Shared.Interfaces;
using src.Modules.ReservationModule.Shared.EndPointGroups;

namespace src.Modules.ReservationModule.Features.DeleteReservation;

public class DeleteReservationEndpoint(IReservationRepository reservationRepository) : EndpointWithoutRequest
<
    Results<Ok, NotFound>
>
{
    public override void Configure()
    {
        Delete("reservation/{id:guid}");
        AllowAnonymous();
        Group<UserEndpointGroup>();
    }

    public override async Task<Results<Ok, NotFound<string>>> HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var result = await reservationRepository.DeleteAsync(id, ct);

        if (!result)
            return TypedResults.NotFound("The reservation you tried to delete was not found.");

        return TypedResults.Ok();
    }
}