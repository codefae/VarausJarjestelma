using System.ComponentModel.DataAnnotations;

namespace src.Modules.ReservationModule.Features.GetRoomInfo;

public class GetRoomInfoRequest
{
    public required string RoomId { get; init; } 
}