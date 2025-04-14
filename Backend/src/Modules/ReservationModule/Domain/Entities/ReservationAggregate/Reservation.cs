using System.Collections.ObjectModel;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

namespace src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;

// TODO The values that are before current date need to be archived or deleted on a backround job for the db.

public class Reservation 
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    
    public ReservationDetails ReservationDetails { get; private set; }
    public DateTime Day { get; private set; }
    public TimeSlot TimeSlot { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    public Guid RoomId { get; private set; }
    public Room Room { get; private set; } = null!;
    // Parameterless constructor for EF Core
#pragma warning disable CS8618, CS9264
    public Reservation()
    {
    }
#pragma warning restore CS8618, CS9264
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
        Guid? deviceId = null,
        Guid? id = null)
    {
        startTime = RoundToNearest15Minutes(startTime);
        endTime = RoundToNearest15Minutes(endTime);
        ValidateStartTimeIsInFuture(day, startTime);

        ReservationDetails reservationDetails;
        if (eventId.HasValue && eventId != Guid.Empty)
            reservationDetails = new ReservationDetails(ReservationType.EventReservation, eventId:eventId.Value);
        else if (deviceId.HasValue && deviceId != Guid.Empty)
            reservationDetails = new ReservationDetails(ReservationType.DeviceReservation, deviceId: deviceId.Value);
        else
            reservationDetails = new ReservationDetails(ReservationType.RoomReservation);

        Id = id ?? Guid.NewGuid();
        UserId = userId;
        RoomId = roomId;
        Day = day.Date;
        TimeSlot = new TimeSlot(startTime, endTime);
        ReservationDetails = reservationDetails;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public bool IsConflicting(
        WeeklySchedule openTimesWeekDays,
        IReadOnlyList<OpenTimeForDay> exceptionsToWeekDayRulesReadOnly,
        DateTime defaultOpenDate,
        DateTime defaultClosingDate)
    {
        if (Day.Date < defaultOpenDate.Date || Day.Date > defaultClosingDate.Date)
            return true;

        var closedOnTimeSlotsConflicts = exceptionsToWeekDayRulesReadOnly
            .Where(dateTimeSlot => dateTimeSlot.Day == Day)
            .Select(x => x.TimeSlot)
            .Where(timeSlot => TimeSlot.IsWithin(timeSlot))
            .ToList();

        var timeSlot = Day.DayOfWeek switch
        {
            DayOfWeek.Sunday => openTimesWeekDays.Sunday,
            DayOfWeek.Monday => openTimesWeekDays.Monday,
            DayOfWeek.Tuesday => openTimesWeekDays.Tuesday,
            DayOfWeek.Wednesday => openTimesWeekDays.Wednesday,
            DayOfWeek.Thursday => openTimesWeekDays.Thursday,
            DayOfWeek.Friday => openTimesWeekDays.Friday,
            DayOfWeek.Saturday => openTimesWeekDays.Saturday,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        if(!TimeSlot.IsWithin(timeSlot))
            return true;

        return closedOnTimeSlotsConflicts.Count != 0;
    }

    public List<Reservation> GetConflicts(List<Reservation> otherReservations)
    {
        return otherReservations.Where(reservation =>
            reservation.RoomId == RoomId &&
            reservation.Day.Date == Day.Date &&
            reservation.TimeSlot.ConflictsWith(TimeSlot) &&
            (
               ( ReservationDetails.Type == ReservationType.RoomReservation&& reservation.ReservationDetails.Type == ReservationType.DeviceReservation) ||
                reservation.ReservationDetails.Type == ReservationType.RoomReservation ||
                reservation.ReservationDetails.Type == ReservationType.EventReservation ||
                (reservation.ReservationDetails.Type == ReservationType.DeviceReservation &&
                 ReservationDetails.Type == ReservationType.DeviceReservation &&
                 reservation.ReservationDetails.DeviceId == ReservationDetails.DeviceId)
            )
        ).ToList();
    }

    public void ChangeReservationTime(DateTime day, TimeSpan newStartTime, TimeSpan newEndTime)
    {
        day = day.Date;
        newStartTime = RoundToNearest15Minutes(newStartTime);
        newEndTime = RoundToNearest15Minutes(newEndTime);

        ValidateStartTimeIsInFuture(day, newStartTime);

        Day = day;
        TimeSlot = new TimeSlot(newStartTime, newEndTime);
        UpdatedAt = DateTime.Now;
    }

    private static TimeSpan RoundToNearest15Minutes(TimeSpan time)
    {
        var minutes = (int)Math.Round(time.TotalMinutes / 15.0) * 15;
        return TimeSpan.FromMinutes(minutes);
    }

    private static void ValidateStartTimeIsInFuture(DateTime day, TimeSpan startTime)
    {
        if (day.Date < DateTime.Now.Date)
            throw new ArgumentException("Start day must be today or in the future.");
        if (day.Date == DateTime.Now.Date && startTime < DateTime.Now.TimeOfDay)
            throw new ArgumentException("Start time must be in the future.");
    }
}