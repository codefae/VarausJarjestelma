using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Features.GetRoomInfo;

public class GetRoomInfoEndpoint(IReservationRepository reservationRepository, IRoomRepository roomRepository) : Endpoint
<
    GetRoomInfoRequest, 
    Results<Ok<GetRoomInfoResponse>, ProblemHttpResult>, 
    GetRoomInfoMapper
>
{
    public override void Configure()
    {
        Get("/rooms/{RoomId}/info");
        Validator<GetRoomInfoRequestValidator>();
        AllowAnonymous();
    }

    public override async Task<Results<Ok<GetRoomInfoResponse>, ProblemHttpResult>> HandleAsync(GetRoomInfoRequest req, CancellationToken ct)
    {
        var roomTask = roomRepository.GetRoomByIdAsync(Guid.Parse(req.RoomId), ct);
        var reservationsTask = reservationRepository.GetByRoomAsync(Guid.Parse(req.RoomId), ct);

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