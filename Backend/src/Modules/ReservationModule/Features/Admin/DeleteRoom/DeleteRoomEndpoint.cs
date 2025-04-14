using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Shared.EndPointGroups;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Features.Admin.DeleteRoom;

public class DeleteRoomEndpoint(
    IUnitOfWork unitOfWork,
    ISendEMailToUser sendEMailToUser) : EndpointWithoutRequest
<
    Results<Ok<string>, NotFound>
>
{
    public override void Configure()
    {
        Delete("room/{roomId:guid}");
        Group<AdminEndpointGroup>();
    }

    public override async Task<Results<Ok<string>, NotFound>> ExecuteAsync(CancellationToken ct)
    {
        var roomId = Route<Guid>("roomId");
        try
        {
            await unitOfWork.BeginTransactionAsync(ct);
            var room = await unitOfWork.Rooms.GetAsync(roomId, ct);
            if (room == null)
                return TypedResults.NotFound();

            var reservations = (await unitOfWork.Reservations.GetByRoomAsync(roomId, ct)).ToList();

            await unitOfWork.Reservations.DeleteManyAsync(reservations.Select(x => x.Id), ct);
            await unitOfWork.Rooms.DeleteRoomAsync(roomId, ct);

            await unitOfWork.CommitTransactionAsync(ct);

            sendEMailToUser.SendEMailToUsers(
                reservations.Select(x => x.UserId).ToList(),
                $"Your reservations for room {room.Name} have been deleted.",
                "You have reservations that have been deleted because a device was removed by an administrator.");

            return TypedResults.Ok($"Device and {reservations.Count()} reservations deleted and emails send to users.");
        }
        catch (Exception e)
        {
            unitOfWork.Dispose();
            throw;
        }
    }
}