using Microsoft.EntityFrameworkCore;

namespace src.Modules.ReservationModule.Domain.Entities;

[Owned]
public class WeeklySchedule
{
    public TimeSlot Monday { get; private set; } =new TimeSlot(TimeSpan.FromHours(8), TimeSpan.FromHours(16));
    public TimeSlot Tuesday { get; private set;} = new TimeSlot(TimeSpan.FromHours(8), TimeSpan.FromHours(16));
    public TimeSlot Wednesday { get; private set; } = new TimeSlot(TimeSpan.FromHours(8), TimeSpan.FromHours(16));
    public TimeSlot Thursday { get; private set;} =  new TimeSlot(TimeSpan.FromHours(8), TimeSpan.FromHours(16));
    public TimeSlot Friday { get; private set;} =  new TimeSlot(TimeSpan.FromHours(8), TimeSpan.FromHours(16));
    public TimeSlot Saturday { get; private set;} =  new TimeSlot(TimeSpan.FromHours(8), TimeSpan.FromHours(16));
    public TimeSlot Sunday { get; private set;} =  new TimeSlot(TimeSpan.FromHours(8), TimeSpan.FromHours(16));

    public WeeklySchedule() { } // EF Core requires a parameterless constructor

    public WeeklySchedule(
        TimeSlot monday, TimeSlot tuesday, TimeSlot wednesday, TimeSlot thursday, 
        TimeSlot friday, TimeSlot saturday, TimeSlot sunday)
    {
        Monday = monday ?? throw new ArgumentNullException(nameof(monday));
        Tuesday = tuesday ?? throw new ArgumentNullException(nameof(tuesday));
        Wednesday = wednesday ?? throw new ArgumentNullException(nameof(wednesday));
        Thursday = thursday ?? throw new ArgumentNullException(nameof(thursday));
        Friday = friday ?? throw new ArgumentNullException(nameof(friday));
        Saturday = saturday ?? throw new ArgumentNullException(nameof(saturday));
        Sunday = sunday ?? throw new ArgumentNullException(nameof(sunday));
    }

    public WeeklySchedule WithTimeSlot(DayOfWeek day, TimeSlot newTimeSlot)
    {
        return day switch
        {
            DayOfWeek.Monday => new WeeklySchedule(newTimeSlot, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday),
            DayOfWeek.Tuesday => new WeeklySchedule(Monday, newTimeSlot, Wednesday, Thursday, Friday, Saturday, Sunday),
            DayOfWeek.Wednesday => new WeeklySchedule(Monday, Tuesday, newTimeSlot, Thursday, Friday, Saturday, Sunday),
            DayOfWeek.Thursday => new WeeklySchedule(Monday, Tuesday, Wednesday, newTimeSlot, Friday, Saturday, Sunday),
            DayOfWeek.Friday => new WeeklySchedule(Monday, Tuesday, Wednesday, Thursday, newTimeSlot, Saturday, Sunday),
            DayOfWeek.Saturday => new WeeklySchedule(Monday, Tuesday, Wednesday, Thursday, Friday, newTimeSlot, Sunday),
            DayOfWeek.Sunday => new WeeklySchedule(Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, newTimeSlot),
            _ => throw new ArgumentOutOfRangeException(nameof(day), "Invalid day")
        };
    }
}
