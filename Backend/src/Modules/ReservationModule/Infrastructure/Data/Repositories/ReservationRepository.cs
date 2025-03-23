using Microsoft.EntityFrameworkCore;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Infrastructure.Data.Repositories;

public class ReservationRepository(ApplicationDbContext context) : IReservationRepository
{
    public async Task<Reservation?> GetAsync(Guid id, CancellationToken cancellationToken) => 
        await context.Reservations.FindAsync([id], cancellationToken);

    public async Task<IEnumerable<Reservation>> GetAllAsync(CancellationToken cancellationToken) =>
        await context.Reservations.ToListAsync(cancellationToken);

    public async Task AddAsync(Reservation reservation, CancellationToken cancellationToken)=> 
        await context.Reservations.AddAsync(reservation, cancellationToken);

    public Task UpdateAsync(Reservation reservation, CancellationToken cancellationToken)
    {
        context.Reservations.Update(reservation);
        return context.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> DeleteAsync(Guid reservationId, CancellationToken cancellationToken)
    {
        context.Reservations.Remove(context.Reservations.Find(reservationId));
        context.SaveChangesAsync(cancellationToken);
    }

    public Task<IEnumerable<Reservation>> GetByRoomAsync(Guid roomId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Reservation>> GetByUserAsync(Guid userId, DateTime date, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Reservation>> GetByRoomAndDateAsync(Guid roomId, DateTime date, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}