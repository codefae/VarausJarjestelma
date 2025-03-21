using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Features.Admin.PostDeviceToRoom;

public class PostDeviceToRoomEndpoint(
    IRoomRepository roomRepository,
    ILogger<PostDeviceToRoomEndpoint> logger)
    : EndpointWithMapper<PostDeviceToRoomRequest, PostDeviceToRoomMapper>
{
    public override void Configure()
    {
        Post("/room/device");
        Validator<PostDeviceToRoomRequestValidator>();
        AllowAnonymous();
    }

    public override async Task<Results<NotFound<string>, Ok<string>, ProblemHttpResult>> HandleAsync(
        PostDeviceToRoomRequest req, CancellationToken ct)
    {
        if (!Guid.TryParse(req.RoomId, out var roomId))
            throw new ArgumentException("Invalid room id format!");

        var device = Map.ToEntity(req);

        var room = await roomRepository.GetRoomByIdAsync(roomId, ct);
        if (room == null)
            return TypedResults.NotFound("Room not found!");
        
        if (room.Devices.Any(d => d.Name == device.Name))
            return TypedResults.Problem("A device with the same name already exists in the room!");
        
        room.Devices.Add(device);
        await roomRepository.UpdateRoomAsync(room, ct);
        return TypedResults.Ok("Device added to room successfully.");
    }
}