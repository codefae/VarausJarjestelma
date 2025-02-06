using FastEndpoints;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;

namespace src.Modules.ReservationModule.Features.PostReservation;

public class PostReservationMapper : RequestMapper<PostReservationRequest, Reservation>
{
    // TODO commended parts are for the event feature
    public override Reservation ToEntity(PostReservationRequest r)
    {
        if (!Guid.TryParse(r.UserId, out var userId)) 
            throw new ArgumentException("Invalid id format!");

        if (!Guid.TryParse(r.ReservationDto.RoomId, out var roomId))
            throw new ArgumentException("Invalid room id format!");
        
        Guid? deviceId = null;
        if (r.ReservationDto.DeviceId != null)
        {
            if (!Guid.TryParse(r.ReservationDto.DeviceId, out var parsedDeviceId))
                throw new ArgumentException("Invalid device id format!");

            deviceId = parsedDeviceId;
        }
        
        // Guid? eventId = null;
        // if (r.ReservationDto.DeviceId != null)
        // {
        //     if (!Guid.TryParse(r.ReservationDto.EventId, out var parsedEventId))
        //         throw new ArgumentException("Invalid event id format!");
        //
        //     eventId = parsedEventId;
        // }
        
        return new Reservation(
            userId,
            roomId,
            r.ReservationDto.StartTime.Date,
            r.ReservationDto.StartTime.TimeOfDay,
            r.ReservationDto.EndTime.TimeOfDay,
            deviceId: deviceId,
            eventId: null 
            // eventId: eventId == Guid.Empty ? null : eventId
        );
    }
}