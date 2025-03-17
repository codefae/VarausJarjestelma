using src.Modules.ReservationModule.Shared.Dtos;

namespace src.Modules.ReservationModule.Features.PatchReservationTime;

public class PatchReservationTimeRequest
{
    public required string ReservationId { get; set; }
    public required DateTime Day { get; set; }
    public required TimeSlotDto TimeSlotDto { get; set; }
}