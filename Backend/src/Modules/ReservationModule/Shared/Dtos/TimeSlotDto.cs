using NSwag.Annotations;

namespace src.Modules.ReservationModule.Shared.Dtos;

public class TimeSlotDto
{
    public required TimeSpan StartTime { get; init; }
    public required TimeSpan EndTime { get; init; }
}