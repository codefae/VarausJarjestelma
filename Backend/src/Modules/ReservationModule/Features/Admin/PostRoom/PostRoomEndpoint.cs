using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Features.Admin.PostRoom;

public class PostRoomEndpoint(IUnitOfWork unitOfWork)
    : Endpoint<PostRoomRequest, Results<Ok, Conflict<string>, ProblemHttpResult>, PostRoomMapper>
{
    public override void Configure()
    {
        Post("/admin/room");
        Validator<PostRoomValidator>();
        AllowAnonymous();
    }

    public override async Task<Results<Ok, Conflict<string>, ProblemHttpResult>> ExecuteAsync(PostRoomRequest req,
        CancellationToken ct)
    {
        await unitOfWork.BeginTransactionAsync(ct);

        var rooms = await unitOfWork.Rooms.GetRoomsAsync(ct);
        if (rooms.Any(r => r.Name == req.Name))
        {
            await unitOfWork.RollbackTransactionAsync(ct);
            return TypedResults.Conflict("Room with the same name already exists");
        }

        var room = Map.ToEntity(req);
        await unitOfWork.Rooms.AddRoomAsync(room, ct);
        await unitOfWork.CommitTransactionAsync(ct);

        return TypedResults.Ok();
    }
}