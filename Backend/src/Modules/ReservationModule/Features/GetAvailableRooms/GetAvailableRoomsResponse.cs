namespace src.Modules.ReservationModule.Features.GetAvailableRooms;

public class GetAvailableRoomsResponse
{
    public required List<RoomDetails> AvailableRooms { get; set; }
}

public class RoomDetails
{
    public required string RoomId { get; set; }
    public required string RoomName { get; set; }
}
