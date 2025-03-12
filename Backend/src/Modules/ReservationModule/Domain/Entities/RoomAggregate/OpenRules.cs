using System.Collections.ObjectModel;

namespace src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

public class OpenRules
{
    /// <summary>
    /// The values that are before current date need to be archived or deleted on a backround job for the db
    /// </summary>
    private Dictionary<DateTime, OpenTimes> _openTimesSingleDay;
    public ReadOnlyDictionary<DateTime, OpenTimes> OpenTimesSingleDaysReadOnly => _openTimesSingleDay.AsReadOnly();
    public DateTime DefaultOpenDate { get; private set; }
    public DateTime DefaultCloseDate { get; private set; }
    private Dictionary<DayOfWeek, OpenTimes> _defaultOpenTimesForWeek;
    public ReadOnlyDictionary<DayOfWeek, OpenTimes> DefaultOpenTimesForWeek => _defaultOpenTimesForWeek.AsReadOnly();
    public DateTime UpdatedAt { get; private set; } 
    
    public OpenRules(
        DateTime defaultOpenDate,
        DateTime defaultCloseDate)
    {
        if (defaultCloseDate < defaultOpenDate)
            throw new ArgumentException("Start time must be before end time");

        DefaultOpenDate = defaultOpenDate;
        DefaultCloseDate = defaultCloseDate;
        _defaultOpenTimesForWeek = new Dictionary<DayOfWeek, OpenTimes>()
        {
            { DayOfWeek.Monday, new OpenTimes(TimeSpan.FromHours(8), TimeSpan.FromHours(16)) },
            { DayOfWeek.Tuesday, new OpenTimes(TimeSpan.FromHours(8), TimeSpan.FromHours(16)) },
            { DayOfWeek.Wednesday, new OpenTimes(TimeSpan.FromHours(8), TimeSpan.FromHours(16)) },
            { DayOfWeek.Thursday, new OpenTimes(TimeSpan.FromHours(8), TimeSpan.FromHours(16)) },
            { DayOfWeek.Friday, new OpenTimes(TimeSpan.FromHours(8), TimeSpan.FromHours(16)) },
            { DayOfWeek.Saturday, new OpenTimes(TimeSpan.FromHours(8), TimeSpan.FromHours(16)) },
            { DayOfWeek.Sunday, new OpenTimes(TimeSpan.FromHours(8), TimeSpan.FromHours(16)) },
        };
        _openTimesSingleDay = [];
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
        _openTimesSingleDay[date] = new OpenTimes(startTime, endTime);
        UpdatedAt = DateTime.Now;
    }

    public void RemoveOpenTimeSingleDay(DateTime date)
    {
        if (_openTimesSingleDay.Remove(date))
        {
            UpdatedAt = DateTime.Now;
        }
    }
    
    public void ChangeDefaultOpenTimeForWeekDay(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
    {
        if (!_defaultOpenTimesForWeek.ContainsKey(dayOfWeek))
            throw new ArgumentException("Invalid day of the week");
        
        _defaultOpenTimesForWeek[dayOfWeek] = new OpenTimes(startTime, endTime);
        UpdatedAt = DateTime.Now;
    }
}
