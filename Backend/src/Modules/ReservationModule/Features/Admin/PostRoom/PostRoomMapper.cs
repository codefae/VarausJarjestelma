using FastEndpoints;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

namespace src.Modules.ReservationModule.Features.Admin.PostRoom;

public class PostRoomMapper : RequestMapper<PostRoomRequest, Room>
{
    public override Room ToEntity(PostRoomRequest r)
    {
        return new Room(r.Name.ToString(), r.DefaultOpenDate, r.DefaultCloseDate, Guid.NewGuid());
    }
}