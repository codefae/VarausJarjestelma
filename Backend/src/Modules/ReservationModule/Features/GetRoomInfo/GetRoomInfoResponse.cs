using src.Modules.ReservationModule.Shared.Dtos;

namespace src.Modules.ReservationModule.Features.GetRoomInfo;

public class GetRoomInfoResponse
{
    public required string RoomId { get; init; } 
    public required string RoomName { get; init; } 
    public required List<DeviceDto> RoomDevices { get; init; }
    public required OpenRulesDto OpenTimes { get; init; } 
    public required List<ReservationDto> ReservationDtos { get; init; } 
}