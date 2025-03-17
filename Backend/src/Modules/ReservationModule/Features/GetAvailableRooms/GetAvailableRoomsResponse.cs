namespace src.Modules.ReservationModule.Features.GetAvailableRooms;

public class GetAvailableRoomsResponse
{
    public required List<(string roomId, string roomName)> AvailableRooms { get; set; }
}