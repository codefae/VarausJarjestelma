using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Shared.Interfaces;
using src.Modules.ReservationModule.Shared.EndPointGroups;
namespace src.Modules.ReservationModule.Features.GetRoomInfo;

public class GetRoomInfoEndpoint(IReservationRepository reservationRepository, IRoomRepository roomRepository)
    : EndpointWithoutRequest
    <
        Results<Ok<GetRoomInfoResponse>, NotFound<string>>,
        GetRoomInfoMapper
    >
{
    public override void Configure()
    {
        Get("rooms/{roomId}/info");
        AllowAnonymous();
        Group<UserEndpointGroup>();
    }

    public override async Task<Results<Ok<GetRoomInfoResponse>,NotFound<string>>> ExecuteAsync(CancellationToken ct)
    {
        var roomId = Route<Guid>("roomId");
        
        var roomTask = roomRepository.GetAsync(roomId, ct);
        var reservationsTask = reservationRepository.GetByRoomAsync(roomId, ct);

        await Task.WhenAll(roomTask, reservationsTask);

        var room = await roomTask;
        var reservations = await reservationsTask;

        if (room == null)
        {
            return TypedResults.NotFound("Room not found");
        }

        var getRoomInfoResponse = Map.FromEntity((reservations.ToList(), room));
        return TypedResults.Ok(getRoomInfoResponse);
    }
}