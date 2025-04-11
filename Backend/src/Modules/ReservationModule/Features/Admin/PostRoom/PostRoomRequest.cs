namespace src.Modules.ReservationModule.Features.Admin.PostRoom;

public class PostRoomRequest
{
    public required string Name {get; init;}
    public DateTime DefaultOpenDate { get; init; }
    public DateTime DefaultCloseDate {get; init;} 
}