using Microsoft.EntityFrameworkCore;
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
        modelBuilder.Entity<Room>().HasKey(r => r.Id);
        modelBuilder.Entity<Room>().OwnsOne(r => r.OpenRules);
        
        modelBuilder.Entity<Reservation>().HasKey(r => r.Id);
        modelBuilder.Entity<Reservation>().OwnsOne(r => r.TimeSlot);
        
        modelBuilder.Entity<Device>().HasKey(r => r.Id);
        modelBuilder.Entity<Device>().OwnsOne(r => r.DeviceType);
        
  
    }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
}

