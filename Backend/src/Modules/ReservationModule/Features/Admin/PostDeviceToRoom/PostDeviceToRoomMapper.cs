using FastEndpoints;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Domain.Entities;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;
using src.Modules.ReservationModule.Shared.Dtos;


namespace src.Modules.ReservationModule.Features.PostDeviceToRoom;



public class PostDeviceToRoomMapper : RequestMapper<PostDeviceToRoomRequest, Device>
{
    public override Device ToEntity(PostDeviceToRoomRequest r)
    {
        if (!Guid.TryParse(r.UserId, out var userId)) 
            throw new ArgumentException("Invalid id format!");

        if (!Guid.TryParse(r.DeviceDto.RoomId, out var roomId))
            throw new ArgumentException("Invalid room id format!");
        
        if (!Guid.TryParse(r.DeviceDto.DeviceId, out var deviceId))
            throw new ArgumentException("Invalid device id format!");

        return new Device(r.DeviceDto.DeviceName, r.DeviceDto.RoomId, r.UserId)
        {
        };
    }
}