using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        await unitOfWork.BeginTransactionAsync();
        
        var rooms = await unitOfWork.Rooms.GetRoomsAsync(ct);
        if (rooms.Any(r => r.Name == req.Name))
        {
            await unitOfWork.RollbackTransactionAsync();
            return TypedResults.Conflict("Room with the same name already exists");
        }
        
        var room = Map.ToEntity(req);
        await unitOfWork.Rooms.AddRoomAsync(room, ct);
        await unitOfWork.CommitTransactionAsync();
        
        return TypedResults.Ok();
    }
}