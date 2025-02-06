namespace src.Modules.ReservationModule.Domain.Entities;

public class OpenTimes
{
    public TimeSpan StartTime { get; }
    public TimeSpan EndTime { get; }

    public OpenTimes(TimeSpan startTime, TimeSpan endTime)
    {
        if(startTime < TimeSpan.Zero)
            throw new ArgumentException("StartTime Cannot Be Negative");
        
        if (endTime > TimeSpan.FromHours(24))
            throw new ArgumentException("EndTime Cannot Be more than 24 hours");
        
        if (startTime > endTime)
            throw new ArgumentException("Start time must be before end time");

        StartTime = startTime;
        EndTime = endTime;
    }
}