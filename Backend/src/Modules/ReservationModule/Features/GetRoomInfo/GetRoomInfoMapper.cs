using System.Web;
using FastEndpoints;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;
using src.Modules.ReservationModule.Shared.Dtos;

namespace src.Modules.ReservationModule.Features.GetRoomInfo;

public class GetRoomInfoMapper : ResponseMapper<GetRoomInfoResponse, (List<Reservation>reservations, Room room)>
{
    public override GetRoomInfoResponse FromEntity((List<Reservation> reservations, Room room) e) => new ()
    {
        RoomId =  e.room.Id.ToString(),
        RoomName = HttpUtility.HtmlEncode(e.room.Name),
        RoomDevices = e.room.Devices.Select(x => new DeviceDto()
        {
            Id = x.Id.ToString(),
            Name = HttpUtility.HtmlEncode(x.Name),
            DeviceType = HttpUtility.HtmlEncode(x.DeviceType),
            Description = HttpUtility.HtmlEncode(x.Description)
        }).ToList(),
            
        ReservationDtos = e.reservations.Select(x => new ReservationDto()
        {
            Id = x.Id.ToString(),
            RoomId = x.RoomId.ToString(),
            ReservationType = x.ReservationType.Type,
            StartTime =x.Day.AddMinutes(x.TimeSlot.StartTime.Minutes),
            EndTime = x.Day.AddMinutes(x.TimeSlot.EndTime.Minutes),
            DeviceId = x.ReservationType is DeviceReservation deviceReservation ? deviceReservation.DeviceId.ToString() : null,
            // TODO This is for the event feature
            // EventId = x.ReservationType is EventReservation eventReservation ? eventReservation.EventId.ToString() : null
        }).ToList(),
            
        OpenTimes = new OpenRulesDto()
        {
            DefaultCloseDate = e.room.OpenRules.DefaultCloseDate,
            DefaultOpenDate = e.room.OpenRules.DefaultOpenDate,
            OpenTimesSingleDays = e.room.OpenRules.ExceptionsToWeekDayRulesReadOnly .ToDictionary(x => x.Key, x =>
                new OpenTimesDto()
                {
                    StartTime = x.Value.StartTime,
                    EndTime = x.Value.EndTime
                }),
            DefaultOpenTimesForWeek = e.room.OpenRules.DefaultOpenTimesForWeek.ToDictionary(x => x.Key, x =>
                new OpenTimesDto()
                {
                    StartTime = x.Value.StartTime,
                    EndTime = x.Value.EndTime
                })
        }
    };
}