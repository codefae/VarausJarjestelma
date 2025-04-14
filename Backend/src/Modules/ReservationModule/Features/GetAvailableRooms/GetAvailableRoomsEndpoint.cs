using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Shared.Interfaces;
using src.Modules.ReservationModule.Shared.EndPointGroups;

namespace src.Modules.ReservationModule.Features.GetAvailableRooms;

public class GetAvailableRoomsEndpoint(IRoomRepository roomRepository) : EndpointWithoutRequest
<
    Results<Ok<GetAvailableRoomsResponse>, NotFound>,
    GetAvailableRoomsMapper
>
{
    public override void Configure()
    {
        Get("rooms");
        Group<UserEndpointGroup>();
    }

    public override async Task<Results<Ok<GetAvailableRoomsResponse>, NotFound>> ExecuteAsync(
        CancellationToken ct)
    {
        var rooms = (await roomRepository.GetRoomsAsync(ct).ConfigureAwait(false)).ToList();
        if (rooms.Count == 0)
        {
            return TypedResults.NotFound();
        }

        var response = Map.FromEntity(rooms);

        return TypedResults.Ok(response);
    }
}