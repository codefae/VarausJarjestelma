using Microsoft.EntityFrameworkCore;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Infrastructure.Data.Repositories;

public class RoomRepository(ApplicationDbContext context) :IRoomRepository
{
    public async Task<Room?> GetRoomByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await context.Rooms.FindAsync([id], cancellationToken).ConfigureAwait(false);

    public async Task<IEnumerable<Room>> GetRoomsAsync(CancellationToken cancellationToken) => 
        await context.Rooms.ToListAsync(cancellationToken).ConfigureAwait(false);

    public async Task AddRoomAsync(Room room, CancellationToken cancellationToken) => 
        await context.Rooms.AddAsync(room, cancellationToken).ConfigureAwait(false);

    public async Task UpdateRoomAsync(Room room, CancellationToken cancellationToken)
    {
        context.Rooms.Update(room);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<bool> DeleteRoomAsync(Guid id, CancellationToken cancellationToken)
    {
        var room = await context.Rooms.FindAsync([id], cancellationToken).ConfigureAwait(false);;
        if(room == null)
            return false;
        
        context.Rooms.Remove(room);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        return true;
    }
}