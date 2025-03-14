
namespace src.Modules.ReservationModule.Shared.Dtos;

public class OpenRulesDto
{
    public required DateTime DefaultOpenDate { get; set; }
    public required DateTime DefaultCloseDate { get; set; }
    public required Dictionary<DateTime, TimeSlotDto> OpenTimesSingleDays { get; set; }
    public required Dictionary<DayOfWeek, TimeSlotDto> DefaultOpenTimesForWeek { get; set; } 
}          
