using FastEndpoints;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;

namespace src.Modules.ReservationModule.Features.PatchReservation;

public class PatchReservationMapper : RequestMapper<PatchReservationTimeRequest, Reservation>
{
    public override Reservation ToEntity(PatchReservationTimeRequest r)
    {
        if (!Guid.TryParse(r.UserId.ToString(), out var userId)) 
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
        
        return new Reservation(
            userId,
            roomId,
            r.ReservationDto.StartTime.Date,
            r.ReservationDto.StartTime.TimeOfDay,
            r.ReservationDto.EndTime.TimeOfDay,
            deviceId: deviceId,
            eventId: null 
        );
    }
}