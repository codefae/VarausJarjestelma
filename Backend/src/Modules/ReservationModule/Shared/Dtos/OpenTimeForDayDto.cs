namespace src.Modules.ReservationModule.Shared.Dtos;

public class OpenTimeForDayDto
{
    public required DateTime Day { get; set; }
    public required TimeSlotDto TimeSlotDto { get; set; }
}