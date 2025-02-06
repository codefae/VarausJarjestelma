using src.Modules.ReservationModule.Domain.DomainServices.ResultEnums;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

namespace src.Modules.ReservationModule.Domain.DomainServices.Interfaces;

public interface IBookingDomainService
{
    ValidateReservationResult ValidateReservation(Reservation reservation, Room room, IEnumerable<Reservation> reservations);
}