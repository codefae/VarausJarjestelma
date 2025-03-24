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
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ReservationType>()
            .HasNoKey()
            .UseTphMappingStrategy() // Use Table-Per-Hierarchy mapping
            .HasDiscriminator<string>("ReservationType")
            .HasValue<RoomReservation>("RoomReservation")
            .HasValue<EventReservation>("EventReservation")
            .HasValue<DeviceReservation>("DeviceReservation");

        modelBuilder.Entity<EventReservation>()
            .Property(e => e.EventId)
            .IsRequired();

        modelBuilder.Entity<DeviceReservation>()
            .Property(d => d.DeviceId)
            .IsRequired();
    
        // Configure the entity that owns OpenRules (assuming it's 'Room')

        
        
        modelBuilder.Entity<Room>()
            .OwnsOne(r => r.OpenRules, owned =>
            {
                // Handling 'ExceptionsToWeekDayRules' as a collection of value objects
                owned.OwnsMany(r => r.ExceptionsToWeekDayRules, exceptions =>
                {
                    exceptions.HasKey("OpenTimeForDay");
                    exceptions.OwnsOne(e => e.TimeSlot);  // No need for HasNoKey() here
                });

                // Handling 'DefaultOpenTimesForWeek' as a collection of value objects
                owned.OwnsMany(r => r.DefaultOpenTimesForWeek, days =>
                {
                    // Each default day has a TimeSlot
                    days.HasKey("WeekDayTimeSlot");
                    days.OwnsOne(d => d.TimeSlot);  // No need for HasNoKey() here
                });
            });

    
    
    
    
    
    
    

        modelBuilder.Entity<Reservation>().OwnsOne(r => r.TimeSlot);
    }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
}