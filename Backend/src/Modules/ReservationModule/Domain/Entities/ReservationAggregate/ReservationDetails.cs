using Microsoft.EntityFrameworkCore;

namespace src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;

public enum ReservationType
{
    RoomReservation,
    EventReservation,
    DeviceReservation
}

[Owned]
public class ReservationDetails
{
    public ReservationType Type { get; private set; }
    public Guid? EventId { get; private set; }
    public Guid? DeviceId { get; private set; }

    public ReservationDetails()
    {
        
    }
    // Constructor that enforces required fields for validation
    public ReservationDetails(ReservationType type, Guid? eventId = null, Guid? deviceId = null)
    {
        Type = type;
        EventId = eventId;
        DeviceId = deviceId;
        if (!IsValid())
            throw new ArgumentException("Invalid reservation type");
    }

    // Method to validate if a reservation has all the required data
    public bool IsValid()
    {
        switch (Type)
        {
            // Define your business logic for valid reservations
            case ReservationType.RoomReservation when (EventId != null || DeviceId != null):
            case ReservationType.EventReservation when (EventId == null || DeviceId != null):
            case ReservationType.DeviceReservation when (DeviceId == null || EventId != null):
                return false;
            default:
                return true;
        }
    }
}
