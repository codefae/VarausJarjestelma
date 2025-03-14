using src.Modules.ReservationModule.Shared.Dtos;

namespace src.Modules.ReservationModule.Features.PatchReservationStartAndEndTimes;

public class PatchReservationTimeRequest
{
    public required string ReservationId { get; set; }
    public required TimeSlotDto TimeSlotDto { get; set; }
}