using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Features.Admin.PostDeviceToRoom;

public class PostDeviceToRoomEndpoint(
    IUnitOfWork unitOfWork)
    : Endpoint<
        PostDeviceToRoomRequest, 
        Results<NotFound<string>, Ok<string>, ProblemHttpResult>,
        PostDeviceToRoomMapper>
{
    public override void Configure()
    {
        Post("/admin/room/device");
        Validator<PostDeviceToRoomRequestValidator>();
        AllowAnonymous();
    }

    public override async Task<Results<NotFound<string>, Ok<string>, ProblemHttpResult>> ExecuteAsync(
        PostDeviceToRoomRequest req, CancellationToken ct)
    {
        if (!Guid.TryParse(req.RoomId, out var roomId))
            throw new ArgumentException("Invalid room id format!");

        var device = Map.ToEntity(req);

        await unitOfWork.BeginTransactionAsync();
        var room = await unitOfWork.Rooms.GetRoomByIdAsync(roomId, ct);
        if (room == null)
        {
            await unitOfWork.RollbackTransactionAsync();
            return TypedResults.NotFound("Room not found!");
        }

        if (room.Devices.Any(d => d.Name == device.Name))
        {
            await unitOfWork.RollbackTransactionAsync();
            return TypedResults.Problem("A device with the same name already exists in the room!");
        }

        room.Devices.Add(device);
        await unitOfWork.Rooms.UpdateRoomAsync(room, ct);
        await unitOfWork.CommitTransactionAsync();

        return TypedResults.Ok("Device added to room successfully.");
    }
}