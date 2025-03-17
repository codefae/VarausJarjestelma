using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace src.Modules.ReservationModule.Features.Admin.PostRoom;

public class PostRoomEndpoint : Endpoint<PostRoomRequest, Results<Ok, Conflict, ProblemHttpResult>, PostRoomMapper>
{
    private readonly IRoomRepository _roomRepository;

    public PostRoomEndpoint(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public override void Configure()
    {
        Post("/Admin/Room/Post");
        Validator<PostRoomValidator>();
        AllowAnonymous();
    }

    public override async Task<Results<Ok, Conflict, ProblemHttpResult>> HandleAsync(PostRoomRequest req, CancellationToken ct)
    {
        try
        {
            var rooms = await _roomRepository.GetRoomsAsync(ct);
            if (rooms.Any(r => r.Name == req.Name.ToString()))
            {
                return TypedResults.Conflict();
            }

            var room = Map.ToEntity(req);
            await _roomRepository.AddRoomAsync(room, ct);
            return TypedResults.Ok();
        }
        catch (DbUpdateConcurrencyException)
        {
            return TypedResults.Problem("A concurrency error occurred while adding the room.");
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(ex.Message);
        }
    }
}