using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace src.Modules.ReservationModule.Features.Admin.DeleteRoom;

public class DeleteRoomEndpoint : Endpoint<
    DeleteRoomRequest, 
    Results<Ok, NotFound, ProblemHttpResult>>
{
    public override void Configure()
    {
        Delete("admin/room");
        Validator<DeleteRoomRequestValidator>();
        AllowAnonymous();
    }

    public override Task HandleAsync(DeleteRoomRequest req, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}