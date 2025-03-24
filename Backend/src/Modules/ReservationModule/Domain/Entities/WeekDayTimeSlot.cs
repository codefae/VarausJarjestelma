namespace src.Modules.ReservationModule.Domain.Entities;

public class WeekDayTimeSlot
{
    public DayOfWeek DayOfWeek { get; private set; }
    public TimeSlot TimeSlot { get; private set; }

    public WeekDayTimeSlot(DayOfWeek dayOfWeek, TimeSlot timeSlot)
    {
        DayOfWeek = dayOfWeek;
        TimeSlot = timeSlot;
    }
}