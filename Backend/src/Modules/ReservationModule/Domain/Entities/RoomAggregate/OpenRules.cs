using System.Collections.ObjectModel;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;

namespace src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

[Owned]
public class OpenRules
{
    public List<OpenTimeForDay> ExceptionsToWeekDayRules { get; private set; }
    public DateTime DefaultOpenDate { get; private set; }
    public DateTime DefaultCloseDate { get; private set; }
    public WeeklySchedule DefaultOpenTimesForWeek { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // Parameterless constructor for EF Core
#pragma warning disable CS8618, CS9264
    public OpenRules()
    {
    }
#pragma warning restore CS8618, CS9264

    public OpenRules(
        DateTime defaultOpenDate,
        DateTime defaultCloseDate)
    {
        ValidateDefaultOpenDateBeforeCloseDate(defaultOpenDate, defaultCloseDate);

        DefaultOpenDate = defaultOpenDate;
        DefaultCloseDate = defaultCloseDate;
        DefaultOpenTimesForWeek = new WeeklySchedule();

        ExceptionsToWeekDayRules = [];
        UpdatedAt = DateTime.Now;
    }

    public void ChangeDefaultOpenAndStartDates(
        DateTime defaultOpenDate,
        DateTime defaultCloseDate)
    {
        ValidateDefaultOpenDateBeforeCloseDate(defaultOpenDate, defaultCloseDate);

        DefaultOpenDate = defaultOpenDate;
        DefaultCloseDate = defaultCloseDate;
        UpdatedAt = DateTime.Now;
    }

    public void AddOrChangeExceptionsToWeekDayRules(DateTime date, TimeSpan startTime, TimeSpan endTime)
    {
         ExceptionsToWeekDayRules.RemoveAll(x => x.Day.Date == date.Date);
         ExceptionsToWeekDayRules.Add(new OpenTimeForDay(date, new TimeSlot(startTime, endTime)));
         UpdatedAt = DateTime.Now;
    }

    public bool RemoveExceptionsToWeekDayRules(DateTime date)
    {
        if (ExceptionsToWeekDayRules.RemoveAll(x => x.Day.Date == date.Date) < 1)
            return false;
        
        UpdatedAt = DateTime.Now;
        return true;
    }

    public void ChangeDefaultOpenTimeForWeekDay(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
    { 
        DefaultOpenTimesForWeek = DefaultOpenTimesForWeek.WithTimeSlot(dayOfWeek,new TimeSlot(startTime, endTime));

        UpdatedAt = DateTime.Now;
    }

    private static void ValidateDefaultOpenDateBeforeCloseDate(DateTime defaultOpenDate, DateTime defaultCloseDate)
    {
        if (defaultCloseDate < defaultOpenDate)
            throw new ArgumentException("Start time must be before end time");
    }
}