using src.Modules.ReservationModule.Shared.Dtos;

namespace src.Modules.ReservationModule.Features.PatchReservation

{
    public class PatchReservationRequest
    {
        public required string ReservationId { get; init; }
        public required DateTime Day { get; init; }
        public required TimeSlotDto TimeSlotDto { get; init; }
    }
}