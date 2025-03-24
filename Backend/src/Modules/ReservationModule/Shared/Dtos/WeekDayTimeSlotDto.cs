using src.Modules.ReservationModule.Domain.Entities;

namespace src.Modules.ReservationModule.Shared.Dtos;

public class WeekDayTimeSlotDto
{
    public required DayOfWeek DayOfWeek { get; set; }
    public required TimeSlotDto TimeSlotDto { get; set; }
}