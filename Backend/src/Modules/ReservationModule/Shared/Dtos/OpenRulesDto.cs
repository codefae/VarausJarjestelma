
using src.Modules.ReservationModule.Domain.Entities;

namespace src.Modules.ReservationModule.Shared.Dtos;

public class OpenRulesDto
{
    public required DateTime DefaultOpenDate { get; set; }
    public required DateTime DefaultCloseDate { get; set; }
    public required List<OpenTimeForDayDto> OpenTimesSingleDays { get; set; }
    public required List<WeekDayTimeSlotDto>DefaultOpenTimesForWeek { get; set; } 
}          
