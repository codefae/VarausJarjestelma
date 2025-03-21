using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;
using src.Modules.ReservationModule.Shared.Dtos;

namespace src.Modules.ReservationModule.Features.Admin.PostRoom;

public class PostRoomRequest
{
    public required string Name {get; set;}
    public DateTime DefaultOpenDate { get; set; }
    public DateTime DefaultCloseDate {get; set;} 
}