using src.Modules.ReservationModule.Shared.Dtos;

namespace src.Modules.ReservationModule.Features.Admin.PatchOpenRulesForRoom;

public class PatchOpenRulesForRoomRequest
{
    public required string RoomId { get; set; }
    public required OpenRulesDto OpenRules { get; set; }
}