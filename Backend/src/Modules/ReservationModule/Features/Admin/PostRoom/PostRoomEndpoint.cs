using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace src.Modules.ReservationModule.Features.Admin.PostRoom;

public class PostRoomEndpoint : Endpoint
<
    PostRoomRequest,
    Results<Ok, Conflict, ProblemHttpResult>,
    PostRoomMapper
>
{
    public override void Configure()
    {
        Post("/Admin/Room/Post");
        Validator<PostRoomValidator>();
        AllowAnonymous();
    }

    public override Task HandleAsync(PostRoomRequest req, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}