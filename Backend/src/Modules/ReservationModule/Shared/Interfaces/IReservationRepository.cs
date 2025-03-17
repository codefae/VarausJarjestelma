using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;

namespace src.Modules.ReservationModule.Shared.Interfaces;

public interface IReservationRepository
{
    /// <summary>
    /// Get a reservation by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Reservation if found, null if not found</returns>
    Task<Reservation?> GetAsync(Guid id);
    Task<IEnumerable<Reservation>> GetAllAsync();
    
    /// <summary>
    /// Throws an DbUpdateConcurrencyException if the room is changed
    /// </summary>
    /// <param name="reservation"></param>
    /// <returns></returns>
    Task AddAndMakeSureRoomIsNotChangedAsync(Reservation reservation);
    Task UpdateAsync(Reservation reservation);
    Task DeleteAsync(Reservation reservation);
    Task<IEnumerable<Reservation>> GetByRoomAsync(Guid roomId);
    Task<IEnumerable<Reservation>> GetByUserAsync(Guid userId, DateTime date);
    Task<IEnumerable<Reservation>> GetByRoomAndDateAsync(Guid roomId, DateTime date);
    Task<Reservation?> GetReservationByIdAsync(Guid reservationId);
    Task UpdateReservationAsync(Reservation reservation);
}
