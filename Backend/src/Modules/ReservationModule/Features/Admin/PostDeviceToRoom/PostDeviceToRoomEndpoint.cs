using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
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

        try
        {
            await unitOfWork.BeginTransactionAsync(ct);

            var room = await unitOfWork.Rooms.GetAsync(roomId, ct);
            if (room == null)
            {
                await unitOfWork.RollbackTransactionAsync(ct);
                return TypedResults.NotFound("Room not found!");
            }
            
            
            if (!room.AddDevice(device))
            {
                await unitOfWork.RollbackTransactionAsync(ct);
                return TypedResults.Problem("A device with the same name already exists in the room!");
            }
            
            await unitOfWork.Rooms.UpdateRoomAsync(room, ct);
            await unitOfWork.CommitTransactionAsync(ct);

            return TypedResults.Ok("Device added to room successfully.");
        }
        finally
        {
            unitOfWork.Dispose();
        } 
    }
}