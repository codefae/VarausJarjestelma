using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Infrastructure.Data.Repositories;

public class RoomRepository(ApplicationDbContext context) :IRoomRepository
{
    public async Task<Room?> GetAsync(Guid id, CancellationToken cancellationToken) =>
        await context.Rooms.Include(x => x.Devices).FirstOrDefaultAsync(x => x.Id == id, cancellationToken).ConfigureAwait(false);

    public async Task<IEnumerable<Room>> GetRoomsAsync(CancellationToken cancellationToken) => 
        await context.Rooms.ToListAsync(cancellationToken).ConfigureAwait(false);

    public async Task AddRoomAsync(Room room, CancellationToken cancellationToken) => 
        await context.Rooms.AddAsync(room, cancellationToken).ConfigureAwait(false);

    public async Task<bool> UpdateRoomAsync(Room updatedRoom, CancellationToken cancellationToken)
    {
        context.Rooms.Update(updatedRoom);
        return await context.SaveChangesAsync(cancellationToken) > 0;
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