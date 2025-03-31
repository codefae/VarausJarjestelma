using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule;
using src.Modules.ReservationModule.Shared.EndPointGroups;

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
        Group<AdminEndpointGroup>();
    }

    public override Task<Results<Ok, NotFound, ProblemHttpResult>> ExecuteAsync(DeleteRoomRequest req, CancellationToken ct)
    {
        throw new System.NotImplementedException();
    }
}