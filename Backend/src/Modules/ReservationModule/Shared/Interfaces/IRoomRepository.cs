using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

namespace src.Modules.ReservationModule.Shared.Interfaces;

public interface IRoomRepository 
{
    /// <summary>
    /// Returns a room by its id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Room if found, null if not found</returns>
    Task<Room?> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<Room>> GetRoomsAsync(CancellationToken cancellationToken);
    Task AddRoomAsync(Room room, CancellationToken cancellationToken);
    Task UpdateRoomAsync(Room room, CancellationToken cancellationToken);
    Task<bool> DeleteRoomAsync(Guid id, CancellationToken cancellationToken);
}