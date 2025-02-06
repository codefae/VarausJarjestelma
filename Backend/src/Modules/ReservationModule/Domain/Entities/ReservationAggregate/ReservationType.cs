namespace src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;

public abstract class ReservationType
{
    public string Type { get; protected init; } = null!;
}

public class RoomReservation : ReservationType
{
    public RoomReservation()
    {
        Type = "RoomReservation";
    }
}

// Not part of mvp?
public class EventReservation : ReservationType
{
    public Guid EventId { get; init; }

    public EventReservation(Guid eventId)
    {
        Type = "EventReservation";
        EventId = eventId;
    }
}
public class DeviceReservation : ReservationType
{
    public Guid DeviceId { get; init; }

    public DeviceReservation(Guid deviceId)
    {
        Type = "DeviceReservation";
        DeviceId = deviceId;
    }
}
