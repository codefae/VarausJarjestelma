using System.Collections.ObjectModel;

namespace src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

public class OpenRules
{
    /// <summary>
    /// The values that are before current date need to be archived or deleted on a backround job for the db
    /// </summary>
    private Dictionary<DateTime, TimeSlot> _closedOnTimeSlots;
    public ReadOnlyDictionary<DateTime, TimeSlot> ClosedOnTimeSlotsReadOnly => _closedOnTimeSlots.AsReadOnly();
    public DateTime DefaultOpenDate { get; private set; }
    public DateTime DefaultCloseDate { get; private set; }
    private Dictionary<DayOfWeek, TimeSlot> _defaultOpenTimesForWeek;
    public ReadOnlyDictionary<DayOfWeek, TimeSlot> DefaultOpenTimesForWeek => _defaultOpenTimesForWeek.AsReadOnly();
    public DateTime UpdatedAt { get; private set; } 
    
    public OpenRules(
        DateTime defaultOpenDate,
        DateTime defaultCloseDate)
    {
        if (defaultCloseDate < defaultOpenDate)
            throw new ArgumentException("Start time must be before end time");

        DefaultOpenDate = defaultOpenDate;
        DefaultCloseDate = defaultCloseDate;
        _defaultOpenTimesForWeek = new Dictionary<DayOfWeek, TimeSlot>()
        {
            { DayOfWeek.Monday, new TimeSlot(TimeSpan.FromHours(8), TimeSpan.FromHours(16)) },
            { DayOfWeek.Tuesday, new TimeSlot(TimeSpan.FromHours(8), TimeSpan.FromHours(16)) },
            { DayOfWeek.Wednesday, new TimeSlot(TimeSpan.FromHours(8), TimeSpan.FromHours(16)) },
            { DayOfWeek.Thursday, new TimeSlot(TimeSpan.FromHours(8), TimeSpan.FromHours(16)) },
            { DayOfWeek.Friday, new TimeSlot(TimeSpan.FromHours(8), TimeSpan.FromHours(16)) },
            { DayOfWeek.Saturday, new TimeSlot(TimeSpan.FromHours(8), TimeSpan.FromHours(16)) },
            { DayOfWeek.Sunday, new TimeSlot(TimeSpan.FromHours(8), TimeSpan.FromHours(16)) },
        };
        _closedOnTimeSlots = [];
        UpdatedAt = DateTime.Now;
    }

    public void ChangeDefaultOpenAndStartDates(
        DateTime defaultOpenDate,
        DateTime defaultCloseDate)
    {
        throw new NotImplementedException();
    }
    
    public void AddOrChangeOpenTimeSingleDay(DateTime date, TimeSpan startTime, TimeSpan endTime) =>
        throw new NotImplementedException();

    public void RemoveOpenTimeSingleDay(DateTime date) =>
        throw new NotImplementedException();
    
    public void ChangeDefaultOpenTimeForWeekDay(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime) => 
        throw new NotImplementedException();
}