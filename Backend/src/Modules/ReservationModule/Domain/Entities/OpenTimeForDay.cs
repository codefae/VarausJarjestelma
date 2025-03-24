using Microsoft.EntityFrameworkCore;

namespace src.Modules.ReservationModule.Domain.Entities;

[Owned]
public class OpenTimeForDay
{
    public DateTime Day{ get; private set; }
    public TimeSlot TimeSlot { get; private set; }

    public OpenTimeForDay()
    {
        
    }
    public OpenTimeForDay(DateTime day, TimeSlot timeSlot)
    {
        Day = day.Date;
        TimeSlot = timeSlot;
    }
}