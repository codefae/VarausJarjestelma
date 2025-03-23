using System.Web;
using FastEndpoints;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

namespace src.Modules.ReservationModule.Features.GetAvailableRooms;

public class GetAvailableRoomsMapper : ResponseMapper<GetAvailableRoomsResponse, List<Room>>
{
    public override GetAvailableRoomsResponse FromEntity(List<Room> rooms)
    {
        return new GetAvailableRoomsResponse
        {
            AvailableRooms = rooms.Select(room =>
                new RoomDetails
                {
                    RoomId = HttpUtility.HtmlEncode(room.Id.ToString()),
                    RoomName = HttpUtility.HtmlEncode(room.Name)
                }).ToList()
        };
    }
}
