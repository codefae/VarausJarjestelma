namespace src.Modules.ReservationModule.Features.GetAvailableRooms;

public class GetAvailableRoomsResponse
{
    public required List<(int roomId, string name)> AvailableRooms { get; set; }
}