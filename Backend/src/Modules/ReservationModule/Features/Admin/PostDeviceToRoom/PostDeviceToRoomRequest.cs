namespace src.Modules.ReservationModule.Features.PostDeviceToRoom
{
    public class PostDeviceToRoomRequest
    {
        public string UserId { get; set; }
        public DeviceDto DeviceDto { get; set; }
    }

    public class DeviceDto
    {
        public string RoomId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
    }
}