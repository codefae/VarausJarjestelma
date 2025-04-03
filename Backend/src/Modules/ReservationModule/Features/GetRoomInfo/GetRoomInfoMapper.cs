using System.Web;
using FastEndpoints;
using src.Modules.ReservationModule.Domain.Entities;
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
            RoomId = x.RoomId.ToString(),
            ReservationType = x.ReservationDetails.Type.ToString(),
            Day = x.Day,
            TimeSlotDto = new TimeSlotDto()
            {
                EndTime = x.TimeSlot.EndTime,
                StartTime = x.TimeSlot.StartTime,
            },
            DeviceId = x.ReservationDetails.Type is ReservationType.DeviceReservation ? x.ReservationDetails.DeviceId.ToString() : null,
            // TODO This is for the event feature
            // EventId = x.ReservationType is EventReservation eventReservation ? eventReservation.EventId.ToString() : null
        }).ToList(),
            
        OpenTimes = new OpenRulesDto()
        {
            DefaultCloseDate = e.room.OpenRules.DefaultCloseDate,
            DefaultOpenDate = e.room.OpenRules.DefaultOpenDate,
            OpenTimesSingleDays = e.room.OpenRules.ExceptionsToWeekDayRules
                .Select(x => new OpenTimeForDayDto
                {
                    Day = x.Day,
                    TimeSlotDto = new TimeSlotDto
                    {
                        StartTime = x.TimeSlot.StartTime,
                        EndTime = x.TimeSlot.EndTime
                    }
                })
                .ToList(),

            DefaultOpenTimesForWeek = new WeeklyScheduleDto( )
            {
                Friday = e.room.OpenRules.DefaultOpenTimesForWeek.Friday,
                Friday = e.room.OpenRules.DefaultOpenTimesForWeek.Friday,
                Saturday = e.room.OpenRules.DefaultOpenTimesForWeek.Saturday,
                Sunday = e.room.OpenRules.DefaultOpenTimesForWeek.Sunday,
                Monday = e.room.OpenRules.DefaultOpenTimesForWeek.Monday,
                Tuesday = e.room.OpenRules.DefaultOpenTimesForWeek.Tuesday,
                Wednesday = e.room.OpenRules.DefaultOpenTimesForWeek.Wednesday,
                Thursday = e.room.OpenRules.DefaultOpenTimesForWeek.Thursday
            }
        }
    };
}