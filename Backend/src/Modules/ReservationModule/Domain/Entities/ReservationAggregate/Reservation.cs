using System.Collections.ObjectModel;

namespace src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;

// TODO The values that are before current date need to be archived or deleted on a backround job for the db.

public class Reservation : IAggregateRoot
{
    public Guid Id { get; }
    public Guid UserId { get; }
    public Guid RoomId { get; }
    public ReservationType ReservationType { get; }
    public DateTime Day { get; private set; }
    public TimeSlot TimeSlot { get;  private set; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; private set; }

    /// <summary>
    /// Creates a reservation.
    /// If eventId is set, it is an event reservation, if eventId is not set, but deviceId is set it is a Device reservation.
    /// Else it is a room reservation.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="roomId"></param>
    /// <param name="day"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="eventId">Set this id for event if event reservation</param>
    /// <param name="deviceId">Set this id for device if device reservation</param>
    public Reservation(
        Guid userId,
        Guid roomId,
        DateTime day,
        TimeSpan startTime,
        TimeSpan endTime,
        Guid? eventId = null,
        Guid? deviceId = null)
    {
        RoundToNearest15Minutes(startTime);
        RoundToNearest15Minutes(endTime);
        day = day.Date;

        if (day < DateTime.Now.Date)
            throw new ArgumentException("Day must be in the future");

        if (startTime <= DateTime.Now.TimeOfDay && day == DateTime.Now.Date)
            throw new ArgumentException("Start time must be in the future if the day is today");

        Id = Guid.NewGuid();
        UserId = userId;
        RoomId = roomId;
        Day = day;
        TimeSlot = new TimeSlot(startTime, endTime);
        if (eventId.HasValue && eventId != Guid.Empty)
        {
            ReservationType = new EventReservation(eventId.Value);
        }
        else if (deviceId.HasValue && deviceId != Guid.Empty)
        {
            ReservationType = new DeviceReservation(deviceId.Value);
        }
        else
        {
            ReservationType = new RoomReservation();
        }
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    private TimeSpan RoundToNearest15Minutes(TimeSpan time)
    {
        var minutes = (int)Math.Round(time.TotalMinutes / 15.0) * 15;
        return TimeSpan.FromMinutes(minutes);
    }

    public bool IsConflicting(
        ReadOnlyDictionary<DayOfWeek, TimeSlot> openTimesWeekDays,
        ReadOnlyDictionary<DateTime, TimeSlot>  exceptionsToWeekDayRulesReadOnly ,
        DateTime defaultOpenDate,
        DateTime defaultClosingDate)
    {
        // Tarkistetaan, onko varaus sallituissa ajoissa
        if (Day > defaultOpenDate || Day < defaultClosingDate)
            return true;

        var closedOnTimeSlotsConflicts = exceptionsToWeekDayRulesReadOnly 
            .Where(dateTimeSlot => dateTimeSlot.Key == Day)
            .Select(x => x.Value)
            .Where(timeSlot => TimeSlot.IsWithin(timeSlot))
            .ToList();
        
        var openTimesWeekdaysConflicts = openTimesWeekDays
            .Where(openTimeWeekDay => openTimeWeekDay.Key == Day.DayOfWeek)
            .Select(openTimeWeekDay => openTimeWeekDay.Value)
            .Where(timeSlot => TimeSlot.IsWithin(timeSlot))
            .ToList();

        return closedOnTimeSlotsConflicts.Count != 0 || openTimesWeekdaysConflicts.Count != 0;
    }
    
    public List<Reservation> GetConflicts(List<Reservation> otherReservations)
    {
        return otherReservations.Where(reservation =>
                reservation.RoomId == RoomId &&
                reservation.Day == Day &&
                reservation.TimeSlot.ConflictsWith(TimeSlot) // Tarkistaa päällekkäisyyden
        ).ToList();
    }

    private void ChangeStartAndEndTime(TimeSpan newStartTime, TimeSpan newEndTime)
    {
        newStartTime = RoundToNearest15Minutes(newStartTime);
        newEndTime = RoundToNearest15Minutes(newEndTime);

        if (Day.Day == DateTime.Now.Day && newStartTime < DateTime.Now.TimeOfDay)
        {
            throw new ArgumentException("Start time must be in the future");
        }
        
        TimeSlot = new TimeSlot(newStartTime, newEndTime);
        UpdatedAt = DateTime.Now;
    }
}