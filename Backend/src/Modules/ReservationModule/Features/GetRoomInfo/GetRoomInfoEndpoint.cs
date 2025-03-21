using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Features.GetRoomInfo;

public class GetRoomInfoEndpoint(IReservationRepository reservationRepository, IRoomRepository roomRepository)
    : EndpointWithoutRequest
    <
        Results<Ok<GetRoomInfoResponse>, ProblemHttpResult>,
        GetRoomInfoMapper
    >
{
    public override void Configure()
    {
        Get("/rooms/{RoomId}/info");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<GetRoomInfoResponse>, ProblemHttpResult>> HandleAsync(CancellationToken ct)
    {
        var roomId = Route<Guid>("RoomId");
        var roomTask = roomRepository.GetRoomByIdAsync(roomId, ct);
        var reservationsTask = reservationRepository.GetByRoomAsync(roomId, ct);

        await Task.WhenAll(roomTask, reservationsTask);

        var room = await roomTask;
        var reservations = await reservationsTask;

        if (room == null)
        {
            return TypedResults.Problem("Room not found");
        }

        var getRoomInfoResponse = Map.FromEntity((reservations.ToList(), room));
        return TypedResults.Ok(getRoomInfoResponse);
    }
}