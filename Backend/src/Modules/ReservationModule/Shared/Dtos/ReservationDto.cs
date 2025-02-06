using System.Text.Json.Serialization;

namespace src.Modules.ReservationModule.Shared.Dtos;

public class ReservationDto
{
    public required string Id { get; init; } 
    public required string RoomId { get; init; } 
    public required string ReservationType { get; init; }
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DeviceId { get; init; }
    
    // TODO reserved for event id
    // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    // public string? EventId { get; init; }
}