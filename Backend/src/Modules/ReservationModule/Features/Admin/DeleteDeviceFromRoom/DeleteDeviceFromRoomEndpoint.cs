using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace src.Modules.ReservationModule.Features.Admin.DeleteDeviceFromRoom;

public class DeleteDeviceFromRoomEndpoint : Endpoint<DeleteDeviceFromRoomRequest, Results<Ok, NotFound>>
{
    public override void Configure()
    {
        throw new NotImplementedException();
    }

    public override Task HandleAsync(DeleteDeviceFromRoomRequest req, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}