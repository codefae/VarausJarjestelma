
namespace src.Modules.ReservationModule.Shared.Dtos;

public class OpenRulesDto
{
    public required DateTime DefaultOpenDate { get; set; }
    public required DateTime DefaultCloseDate { get; set; }
    public required Dictionary<DateTime, OpenTimesDto> OpenTimesSingleDays { get; set; }
    public required Dictionary<DayOfWeek, OpenTimesDto> DefaultOpenTimesForWeek { get; set; } 
}          
