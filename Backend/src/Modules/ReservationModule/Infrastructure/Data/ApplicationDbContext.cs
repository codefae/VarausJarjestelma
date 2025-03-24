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

        // Configure the entity that owns OpenRules (assuming it's 'Room')
        modelBuilder.Entity<Room>(entity =>
        {
            // Configure OpenRules as an owned entity
            entity.OwnsOne(r => r.OpenRules, owned =>
            {
                
            });
        });
    }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
}