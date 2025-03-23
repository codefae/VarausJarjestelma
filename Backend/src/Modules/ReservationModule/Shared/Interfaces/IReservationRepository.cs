using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;

namespace src.Modules.ReservationModule.Shared.Interfaces;

public interface IReservationRepository 
{
    /// <summary>
    /// Get a reservation by its id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Reservation if found, null if not found</returns>
    Task<Reservation?> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<Reservation>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Throws an DbUpdateConcurrencyException if the room is changed
    /// </summary>
    /// <param name="reservation"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddAndMakeSureRoomIsNotChangedAsync(Reservation reservation, CancellationToken cancellationToken);

    Task UpdateAndMakeSureRoomIsNotChangedAsync(Reservation reservation, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid reservationId, CancellationToken cancellationToken);
    Task<IEnumerable<Reservation>> GetByRoomAsync(Guid roomId, CancellationToken cancellationToken);
    Task<IEnumerable<Reservation>> GetByUserAsync(Guid userId, DateTime date, CancellationToken cancellationToken);

    Task<IEnumerable<Reservation>> GetByRoomAndDateAsync(Guid roomId, DateTime date,
        CancellationToken cancellationToken);
}