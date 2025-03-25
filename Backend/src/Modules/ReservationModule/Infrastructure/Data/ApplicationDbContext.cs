using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using src.Modules.ReservationModule.Domain.Entities;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

namespace src.Modules.ReservationModule.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure DefaultOpenTimesForWeek to own WeekDayTimeSlot
        modelBuilder.Entity<Room>()
            .OwnsOne(d => d.OpenRules, openrules =>
            {
                openrules.OwnsMany<OpenTimeForDay>(o => o.ExceptionsToWeekDayRules, exceptionsToWeekDayRules =>
                    exceptionsToWeekDayRules.OwnsOne<TimeSlot>(t => t.TimeSlot));
                openrules.OwnsOne<WeeklySchedule>(o => o., defaultOpenTimesForWeek =>
                    defaultOpenTimesForWeek.OwnsOne<TimeSlot>(t => t.TimeSlot));
            });


        // Or configure the reverse if needed:
        // modelBuilder.Entity<WeekDayTimeSlot>()
        //     .OwnsOne(w => w.OpenRules);
    }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
}