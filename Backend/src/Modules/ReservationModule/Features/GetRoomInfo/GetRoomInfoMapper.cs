using System.Web;
using FastEndpoints;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;
using src.Modules.ReservationModule.Shared.Dtos;

namespace src.Modules.ReservationModule.Features.GetRoomInfo;

public class GetRoomInfoMapper : ResponseMapper<GetRoomInfoResponse, (List<Reservation>reservations, Room room)>
{
    public override GetRoomInfoResponse FromEntity((List<Reservation> reservations, Room room) e) => new()
    {
        RoomId = e.room.Id.ToString(),
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
            DeviceId = x.ReservationDetails.Type is ReservationType.DeviceReservation
                ? x.ReservationDetails.DeviceId.ToString()
                : null,
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

            DefaultOpenTimesForWeek = new WeeklyScheduleDto()
            {
                Monday = new TimeSlotDto()
                {
                    StartTime = e.room.OpenRules.DefaultOpenTimesForWeek.Monday.StartTime,
                    EndTime = e.room.OpenRules.DefaultOpenTimesForWeek.Monday.EndTime,
                },
                Tuesday = new TimeSlotDto()
                {
                    StartTime = e.room.OpenRules.DefaultOpenTimesForWeek.Tuesday.StartTime,
                    EndTime = e.room.OpenRules.DefaultOpenTimesForWeek.Tuesday.EndTime,
                },
                Wednesday = new TimeSlotDto()
                {
                    StartTime = e.room.OpenRules.DefaultOpenTimesForWeek.Wednesday.StartTime,
                    EndTime = e.room.OpenRules.DefaultOpenTimesForWeek.Wednesday.EndTime,
                },
                Thursday = new TimeSlotDto()
                {
                    StartTime = e.room.OpenRules.DefaultOpenTimesForWeek.Thursday.StartTime,
                    EndTime = e.room.OpenRules.DefaultOpenTimesForWeek.Thursday.EndTime
                },
                Friday = new TimeSlotDto()
                {
                    StartTime = e.room.OpenRules.DefaultOpenTimesForWeek.Friday.StartTime,
                    EndTime = e.room.OpenRules.DefaultOpenTimesForWeek.Friday.EndTime
                },
                Saturday = new TimeSlotDto()
                {
                    StartTime = e.room.OpenRules.DefaultOpenTimesForWeek.Saturday.StartTime,
                    EndTime = e.room.OpenRules.DefaultOpenTimesForWeek.Saturday.EndTime
                },
                Sunday = new TimeSlotDto()
                {
                    StartTime = e.room.OpenRules.DefaultOpenTimesForWeek.Sunday.StartTime,
                    EndTime = e.room.OpenRules.DefaultOpenTimesForWeek.Sunday.EndTime
                }
            }
        }
    };
}