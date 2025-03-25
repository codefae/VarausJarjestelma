using Microsoft.EntityFrameworkCore;

namespace src.Modules.ReservationModule.Domain.Entities;

[Owned]
public class WeeklySchedule
{
    public TimeSlot Monday { get; private set; }
    public TimeSlot Tuesday { get; private set;}
    public TimeSlot Wednesday { get; private set;}
    public TimeSlot Thursday { get; private set;}
    public TimeSlot Friday { get; private set;}
    public TimeSlot Saturday { get; private set;}
    public TimeSlot Sunday { get; private set;}

    private WeeklySchedule() { } // EF Core requires a parameterless constructor

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
    public WeeklySchedule(TimeSlot defaultTimeSlot)
    {
        Monday = defaultTimeSlot;
        Tuesday = defaultTimeSlot;
        Wednesday = defaultTimeSlot;
        Thursday = defaultTimeSlot;
        Friday = defaultTimeSlot;
        Saturday = defaultTimeSlot;
        Sunday = defaultTimeSlot;
    }

    public TimeSlot GetTimeSlot(DayOfWeek day) => day switch
    {
        DayOfWeek.Monday => Monday,
        DayOfWeek.Tuesday => Tuesday,
        DayOfWeek.Wednesday => Wednesday,
        DayOfWeek.Thursday => Thursday,
        DayOfWeek.Friday => Friday,
        DayOfWeek.Saturday => Saturday,
        DayOfWeek.Sunday => Sunday,
        _ => throw new ArgumentOutOfRangeException(nameof(day), "Invalid day")
    };

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

    public override bool Equals(object? obj)
    {
        if (obj is not WeeklySchedule other) return false;
        return Monday.Equals(other.Monday) &&
               Tuesday.Equals(other.Tuesday) &&
               Wednesday.Equals(other.Wednesday) &&
               Thursday.Equals(other.Thursday) &&
               Friday.Equals(other.Friday) &&
               Saturday.Equals(other.Saturday) &&
               Sunday.Equals(other.Sunday);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday);
    }
}
