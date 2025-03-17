using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

namespace src.Modules.ReservationModule.Shared.Interfaces;

public interface IRoomRepository
{
    /// <summary>
    /// Returns a room by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Room if found, null if not found</returns>
    Task<Room?> GetRoomByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Room>> GetRoomsAsync(CancellationToken ct);
    Task AddRoomAsync(Room room, CancellationToken cancellationToken);
    Task UpdateRoomAsync(Room room, CancellationToken cancellationToken);
    Task DeleteRoomAsync(Room room, CancellationToken cancellationToken);
}
