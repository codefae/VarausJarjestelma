using Microsoft.EntityFrameworkCore;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Infrastructure.Data.Repositories;

public class ReservationRepository(ApplicationDbContext context) : IReservationRepository
{
    public async Task<Reservation?> GetAsync(Guid id, CancellationToken cancellationToken) =>
        await context.Reservations.FindAsync([id], cancellationToken).ConfigureAwait(false);

    public async Task<IEnumerable<Reservation>> GetAllAsync(CancellationToken cancellationToken) =>
        await context.Reservations.ToListAsync(cancellationToken).ConfigureAwait(false);

    public async Task AddAsync(Reservation reservation, CancellationToken cancellationToken) =>
        await context.Reservations.AddAsync(reservation, cancellationToken).ConfigureAwait(false);

    public async Task UpdateAsync(Reservation reservation, CancellationToken cancellationToken)
    {
        context.Reservations.Update(reservation);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<bool> DeleteAsync(Guid reservationId, CancellationToken cancellationToken)
    {
        var reservation =
            await context.Reservations.FindAsync([reservationId], cancellationToken).ConfigureAwait(false);
        if (reservation == null)
            return false;

        context.Reservations.Remove(reservation);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return true;
    }

    public async Task DeleteManyAsync(IEnumerable<Guid> reservationIds, CancellationToken ct)
    {
        var reservationsToDelete = await context.Reservations
            .Where(r => reservationIds.Contains(r.Id))
            .ToListAsync(ct);

        if (reservationsToDelete.Count != 0)
        {
            context.Reservations.RemoveRange(reservationsToDelete);
            await context.SaveChangesAsync(ct);
        }
    }
    
    public async Task<IEnumerable<Reservation>> GetByRoomAsync(Guid roomId, CancellationToken cancellationToken) =>
        await context.Reservations.Where(r => r.RoomId == roomId).ToListAsync(cancellationToken).ConfigureAwait(false);


    public async Task<IEnumerable<Reservation>> GetByUserAsync(Guid userId, DateTime date,
        CancellationToken cancellationToken) =>
        await context.Reservations.Where(r => r.UserId == userId).ToListAsync(cancellationToken).ConfigureAwait(false);

    public async Task<IEnumerable<Reservation>> GetByRoomAndDateAsync(Guid roomId, DateTime date,
        CancellationToken cancellationToken) =>
        await context.Reservations.Where(r => r.RoomId == roomId && r.Day == date).ToListAsync(cancellationToken)
            .ConfigureAwait(false);
}