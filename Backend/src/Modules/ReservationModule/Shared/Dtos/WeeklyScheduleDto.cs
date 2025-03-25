using src.Modules.ReservationModule.Domain.Entities;

namespace src.Modules.ReservationModule.Shared.Dtos;

public class WeeklyScheduleDto
{
    public required TimeSlot Monday { get; init; }
    public required TimeSlot Tuesday { get; init;}
    public required TimeSlot Wednesday { get; init;}
    public required TimeSlot Thursday { get; init;}
    public required TimeSlot Friday { get; init;}
    public required TimeSlot Saturday { get; init;}
    public required TimeSlot Sunday { get; init;}
}