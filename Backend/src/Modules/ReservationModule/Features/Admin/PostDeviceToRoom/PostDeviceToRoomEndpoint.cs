using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Features.Admin.PostDeviceToRoom;

public class PostDeviceToRoomEndpoint(IRoomRepository roomRepository) : Endpoint<
    PostDeviceToRoomRequest,
    Results<Ok, Results<Ok, Conflict, ProblemHttpResult>>,
    PostDeviceToRoomMapper>
{
    public override void Configure()
    {
        Post("admin/device");
        Validator<PostDeviceToRoomValidator>();
        AllowAnonymous();
    }

    public override Task HandleAsync(PostDeviceToRoomRequest req, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}