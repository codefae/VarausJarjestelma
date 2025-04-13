using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Shared.Interfaces;
using src.Modules.ReservationModule.Shared.EndPointGroups;

namespace src.Modules.ReservationModule.Features.Admin.PostRoom;

public class PostRoomEndpoint(IUnitOfWork unitOfWork)
    : Endpoint
    <
        PostRoomRequest, 
        Results<Ok, Conflict<string>, ProblemHttpResult>, 
        PostRoomMapper
    >
{
    public override void Configure()
    {
        Post("room");
        Validator<PostRoomValidator>();
        AllowAnonymous();
        Group<AdminEndpointGroup>();
    }

    public override async Task<Results<Ok, Conflict<string>, ProblemHttpResult>> ExecuteAsync(PostRoomRequest req,
        CancellationToken ct)
    {
        try
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
        catch
        {
            unitOfWork.Dispose();
            return TypedResults.Problem();
        }
    }
}