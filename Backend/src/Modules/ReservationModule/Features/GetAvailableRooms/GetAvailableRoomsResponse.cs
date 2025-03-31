namespace src.Modules.ReservationModule.Features.GetAvailableRooms;

public class GetAvailableRoomsResponse
{
    public required List<RoomDetails> AvailableRooms { get; init; }
}

public class RoomDetails
{
    public required string RoomId { get; init; }
    public required string RoomName { get; init; }
}
