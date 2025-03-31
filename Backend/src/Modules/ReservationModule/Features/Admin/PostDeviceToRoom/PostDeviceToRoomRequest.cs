using src.Modules.ReservationModule.Shared.Dtos;

namespace src.Modules.ReservationModule.Features.Admin.PostDeviceToRoom
{
    public class PostDeviceToRoomRequest
    {
        public required string RoomId { get; init; }
        public required string Name { get; init; } 
        public required string DeviceType { get; init; } 
        public required string Description { get; init; } 

        
    }
}