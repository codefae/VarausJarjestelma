using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

namespace src.Modules.ReservationModule.Shared.Interfaces;

public interface IRoomRepository
{
    /// <summary>
    /// Returns a room by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Room if found, null if not found</returns>
    Task<Room?> GetRoomByIdAsync(Guid id);
    Task<IEnumerable<Room>> GetRoomsAsync(CancellationToken ct);
    Task AddRoomAsync(Room room);
    Task UpdateRoomAsync(Room room);
    Task DeleteRoomAsync(Room room);
}
