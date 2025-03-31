using System.Threading.Tasks;
using src.Modules.ReservationModule.Shared.Dtos;
using System.Threading.Tasks;
using src.Modules.ReservationModule.Shared.Interfaces;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

namespace src.Modules.ReservationModule.Features.PatchReservation


{
    public class PatchReservationRequest
    {
        public required string ReservationId { get; init; }
        public required DateTime Day { get; init; }
        public required TimeSlotDto TimeSlotDto { get; init; }
    }
}