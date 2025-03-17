using FastEndpoints;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

namespace src.Modules.ReservationModule.Features.Admin.PostRoom;

public class PostRoomMapper : RequestMapper<PostRoomRequest, Room>
{
    public override Room ToEntity(PostRoomRequest req)
    {
        return new Room(req.Name, null, null, Guid.NewGuid());
    }
}