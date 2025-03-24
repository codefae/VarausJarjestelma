using src.Modules.ReservationModule.Domain.DomainServices.Interfaces;
using src.Modules.ReservationModule.Domain.DomainServices.ResultEnums;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

namespace src.Modules.ReservationModule.Domain.DomainServices;

public class BookingDomainService : IBookingDomainService
{
    // TODO When and if events are introduces, check if event exists by passing an extra parameter to this methodm
    // also make sure that event is owned by the user
    public ValidateReservationResult ValidateReservation(Reservation reservation, Room room, IEnumerable<Reservation> reservations)
    {
        switch (reservation.ReservationType)
        {
            case DeviceReservation deviceReservation:
            {
                var isDeviceInDevicesList = room.IsDeviceWithIdInDevicesList(deviceReservation.DeviceId);
                if (!isDeviceInDevicesList)
                    return ValidateReservationResult.DeviceNotFound;
                
                break;
            }
            case EventReservation eventReservation:
                throw new NotImplementedException();
        }

        var isConflicting = reservation.IsConflicting(
            room.OpenRules.DefaultOpenTimesForWeek, 
            room.OpenRules. ExceptionsToWeekDayRules ,
            room.OpenRules.DefaultOpenDate,
            room.OpenRules.DefaultCloseDate);
        if(isConflicting)
            return ValidateReservationResult.RoomNotOpen;
        
        var conflicts = reservation.GetConflicts(reservations.ToList());
        if(conflicts.Count != 0)
            return ValidateReservationResult.ReservationConflicts;
        
        return ValidateReservationResult.Success;
    }
}