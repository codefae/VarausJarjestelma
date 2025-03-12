using System.Collections.ObjectModel;

namespace src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

public class OpenRules
{
    /// <summary>
    /// The values that are before current date need to be archived or deleted on a backround job for the db
    /// </summary>
    private Dictionary<DateTime, TimeSlot> _exceptionsToWeekDayRules;
    public ReadOnlyDictionary<DateTime, TimeSlot> ExceptionsToWeekDayRulesReadOnly => _exceptionsToWeekDayRules.AsReadOnly();
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
        _exceptionsToWeekDayRules = [];
        UpdatedAt = DateTime.Now;
    }

    public void ChangeDefaultOpenAndStartDates(
        DateTime defaultOpenDate,
        DateTime defaultCloseDate)
    {
        if (defaultCloseDate < defaultOpenDate)
            throw new ArgumentException("Start time must be before end time");

        DefaultOpenDate = defaultOpenDate;
        DefaultCloseDate = defaultCloseDate;
        UpdatedAt = DateTime.Now;
    }
    
    public void AddOrChangeOpenTimeSingleDay(DateTime date, TimeSpan startTime, TimeSpan endTime)
    {
        _exceptionsToWeekDayRules[date] = new TimeSlot(startTime, endTime);
        UpdatedAt = DateTime.Now;
    }

    public void RemoveOpenTimeSingleDay(DateTime date)
    {
        if (_exceptionsToWeekDayRules.Remove(date))
        {
            UpdatedAt = DateTime.Now;
        }
    }
    
    public void ChangeDefaultOpenTimeForWeekDay(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
    {
        if (endTime <= startTime)
            throw new ArgumentException("End time must be after start time");
        
        if (!_defaultOpenTimesForWeek.ContainsKey(dayOfWeek))
            throw new ArgumentException("Invalid day of the week");
        
        _defaultOpenTimesForWeek[dayOfWeek] = new TimeSlot(startTime, endTime);
        UpdatedAt = DateTime.Now;
    }
}
