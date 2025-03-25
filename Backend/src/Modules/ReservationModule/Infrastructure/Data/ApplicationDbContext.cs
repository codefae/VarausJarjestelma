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
        base.OnModelCreating(modelBuilder);
     

        // Or configure the reverse if needed:
        // modelBuilder.Entity<WeekDayTimeSlot>()
        //     .OwnsOne(w => w.OpenRules);
    }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
}