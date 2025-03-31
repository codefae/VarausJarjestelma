using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Shared.Interfaces;
using src.Modules.ReservationModule.Shared.EndPointGroups;

namespace src.Modules.ReservationModule.Features.Admin.DeleteDeviceFromRoom;

public class DeleteDeviceFromRoomEndpoint(
    IUnitOfWork unitOfWork,
    ISendEMailToUser sendEMailToUser) : EndpointWithoutRequest<Results<Ok<string>, NotFound<string>>>
{
    public override void Configure()
    {
       Delete("room/{roomId:guid}/device/{deviceId:guid}");
       Group<AdminEndpointGroup>();

    }

    public override async Task<Results<Ok<string>, NotFound<string>>> ExecuteAsync(CancellationToken ct)
    {
        var roomId = Route<Guid>("roomId");
        var deviceId = Route<Guid>("deviceId");
        
        
        try
        {
            await unitOfWork.BeginTransactionAsync(ct);
            var room = await unitOfWork.Rooms.GetAsync(roomId, ct);
            if (room is null)
                return TypedResults.NotFound("Room not found.");
            
            if(!room.RemoveDevice(deviceId))
                return TypedResults.NotFound("Device not found.");
            
            var reservations = await unitOfWork.Reservations.GetByRoomAsync(roomId, ct);
            var deviceReservationsGroups = reservations
                .Where(x => x.ReservationDetails.Type == ReservationType.DeviceReservation)
                .GroupBy(x => x.ReservationDetails.DeviceId == deviceId)
                .ToList();


            var deletableReservations = deviceReservationsGroups
                .FirstOrDefault(group => group.Key); 

            if (deletableReservations != null)
            {
                await unitOfWork.Reservations.DeleteManyAsync(deletableReservations.Select(x => x.UserId), ct);
                
                await unitOfWork.CommitTransactionAsync(ct);
                
                sendEMailToUser.SendEMailToUsers(
                    deletableReservations.Select(x => x.UserId).ToList(),
                    $"Yor reservations for device in room: {room.Name} have been deleted.", 
                    "You have reservations that have been deleted because a device was removed by an administrator.");
                
                return TypedResults.Ok($"Device and {deletableReservations.Count()} reservations deleted and emails send to users.");
            }
            else
            {
                await unitOfWork.CommitTransactionAsync(ct);
                return TypedResults.Ok("Device was deleted, no conflicting resevations.");
            }
            
        }
        finally
        {
            unitOfWork.Dispose();
        }
    }
}