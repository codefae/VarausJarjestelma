using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Features.PostDeviceToRoom;

public class PostDeviceToRoomEndpoint(
    IRoomRepository roomRepository,
    ILogger<PostDeviceToRoomEndpoint> logger)
    : EndpointWithMapper<PostDeviceToRoomRequest, PostDeviceToRoomMapper>
{
    public override void Configure()
    {
        Post("/devices/rooms");
        Validator<PostDeviceToRoomRequestValidator>();
        AllowAnonymous();
    }

    public override async Task<Results<NotFound<string>, Ok<string>, ProblemHttpResult>> HandleAsync(PostDeviceToRoomRequest req, CancellationToken ct)
    {
        var device = Map.ToEntity(req);
        
        var retries = 5; 
        while (retries-- > 0) 
        {
            var room = await roomRepository.GetRoomByIdAsync(device.RoomId, ct);
            if (room == null)
            {
                return TypedResults.NotFound("Room not found!");
            }

            
            if (room.Devices.Any(d => d.Name == device.Name))
            {
                return TypedResults.Problem("A device with the same name already exists in the room!");
            }

            room.Devices.Add(device);

            try
            {
                
                await roomRepository.UpdateRoomAsync(room, ct);
                return TypedResults.Ok("Device added to room successfully.");
            }
            catch (DbUpdateConcurrencyException e)
            {
                logger.LogInformation("Concurrency exception occurred while adding device to room, retrying...");
            }
        }

        return TypedResults.Problem("Many people try to add devices to rooms at the same time, try again later!");
    }
}