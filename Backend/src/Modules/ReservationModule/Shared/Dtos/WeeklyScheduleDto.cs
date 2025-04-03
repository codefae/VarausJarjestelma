using src.Modules.ReservationModule.Domain.Entities;

namespace src.Modules.ReservationModule.Shared.Dtos;

public class WeeklyScheduleDto
{
    public required TimeSlotDto Monday { get; init; }
    public required TimeSlotDto Tuesday { get; init;}
    public required TimeSlotDto Wednesday { get; init;}
    public required TimeSlotDto Thursday { get; init;}
    public required TimeSlotDto Friday { get; init;}
    public required TimeSlotDto Saturday { get; init;}
    public required TimeSlotDto Sunday { get; init;}
}