using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

namespace src.Modules.ReservationModule.Features.Admin.DeleteDeviceFromRoom;

public class DeleteDeviceFromRoomRequest
{
    public required string RoomId { get; init; }
    public required string DeviceId { get; init; }
}