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
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }

    public DateTime CreatedAt { get; } 
    public DateTime UpdatedAt { get; private set; }

public class OpenTimes
{
    public TimeSpan OpeningTime { get; set; }
    public TimeSpan ClosingTime { get; set; }

}

    /// <summary>
    /// Creates a reservation.
    /// If eventId is set, it is an event reservation, if eventId is not set, but deviceId is set it is a Device reservation.
    /// Else it is a room reservation.
    /// </summary>
    /// <param name="id"></param>
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
        
        if(StartTime < TimeSpan.FromHours(0))
            throw new ArgumentException("Start time must be positive");
        
        if(EndTime > TimeSpan.FromHours(24))
            throw new ArgumentException("End time must be less than 24 hours");
        
        if (startTime > endTime)
            throw new ArgumentException("Start time must be before end time");

        if (day < DateTime.Now.Date)
            throw new ArgumentException("Day must be in the future");

        if (startTime <= DateTime.Now.TimeOfDay && day == DateTime.Now.Date)
            throw new ArgumentException("Start time must be in the future if the day is today");

        Id = Guid.NewGuid();
        UserId = userId;
        RoomId = roomId;
        Day = day;
        StartTime = startTime;
        EndTime = endTime;
        
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
        
        var dateTimeNow = DateTime.Now;
        CreatedAt = dateTimeNow;
        UpdatedAt = dateTimeNow;
    }
    private TimeSpan RoundToNearest15Minutes(TimeSpan time)
{
    int minutes = (int)Math.Round(time.TotalMinutes / 15.0) * 15;
    return TimeSpan.FromMinutes(minutes);
}

public bool IsConflicting(
    ReadOnlyDictionary<DayOfWeek, OpenTimes> openTimesWeekDays,
    ReadOnlyDictionary<DateTime, OpenTimes> openTimesSingleDays,
    DateTime defaultOpenDate,
    DateTime defaultClosingDate)
{
    // Tarkistetaan, onko varaus sallituissa ajoissa
    if (Day < defaultOpenDate || Day > defaultClosingDate)
        return true;

    if (!openTimesSingleDays.TryGetValue(Day, out var openTimes))
    {
        openTimesWeekDays.TryGetValue(Day.DayOfWeek, out openTimes);
    }

    if (openTimes == null)
        return true; // Ei aukioloaikoja, eli oletetaan konflikti

    return StartTime < openTimes.OpeningTime || EndTime > openTimes.ClosingTime;
}


    public List<Reservation> GetConflicts(List<Reservation> otherReservations)
{
    return otherReservations.Where(r =>
        r.RoomId == RoomId &&
        r.Day == Day &&
        (r.StartTime < EndTime && r.EndTime > StartTime) // Tarkistaa päällekkäisyyden
    ).ToList();
}

    private void ChangeStartAndEndTime(TimeSpan newStartTime, TimeSpan newEndTime)
{
    newStartTime = RoundToNearest15Minutes(newStartTime);
    newEndTime = RoundToNearest15Minutes(newEndTime);

    if (newStartTime >= newEndTime)
        throw new ArgumentException("Start time must be before end time");

    StartTime = newStartTime;
    EndTime = newEndTime;
    UpdatedAt = DateTime.Now;
}
}