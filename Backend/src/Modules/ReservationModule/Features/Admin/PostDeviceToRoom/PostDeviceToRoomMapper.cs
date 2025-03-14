using FastEndpoints;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

namespace src.Modules.ReservationModule.Features.Admin.PostDeviceToRoom;

public class PostDeviceToRoomMapper : RequestMapper<PostDeviceToRoomRequest, Device>
{
    public override Device ToEntity(PostDeviceToRoomRequest r)
    {
        throw new NotImplementedException();
    }
}