using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

namespace src.Modules.ReservationModule.Features.Admin.DeleteDeviceFromRoom;

public class DeleteDeviceFromRoomRequest
{
    public required string RoomId { get; set; }
    public required string DeviceId { get; set; }
}