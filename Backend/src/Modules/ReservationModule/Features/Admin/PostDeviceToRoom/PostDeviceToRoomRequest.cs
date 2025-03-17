using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Shared.Dtos;

namespace src.Modules.ReservationModule.Features.Admin.PostDeviceToRoom;

public class PostDeviceToRoomRequest
{
    public required string RoomId { get; set; }
    public required DeviceDto DeviceDto { get; set; }
}