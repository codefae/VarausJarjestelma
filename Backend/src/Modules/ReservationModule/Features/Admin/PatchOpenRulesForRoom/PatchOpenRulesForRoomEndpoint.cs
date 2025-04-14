using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Shared.EndPointGroups;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Features.Admin.PatchOpenRulesForRoom;

public class PatchOpenRulesForRoomEndpoint(IUnitOfWork unitOfWork, ISendEMailToUser sendEMailToUser) : Endpoint<
    PatchOpenRulesForRoomRequest,
    Results<Ok, NotFound>>
{
    public override void Configure()
    {
        Patch("rooms/openrules");
        Group<AdminEndpointGroup>();
    }

    public override async Task<Results<Ok, NotFound>> ExecuteAsync(PatchOpenRulesForRoomRequest req, CancellationToken ct)
    {
        try
        {
            await unitOfWork.BeginTransactionAsync(ct);
            
            var room  = await unitOfWork.Rooms.GetAsync(Guid.Parse( req.RoomId), ct);
            if(room == null)
                return TypedResults.NotFound();
            
            room.OpenRules.ChangeDefaultOpenTimeForWeekDay(DayOfWeek.Monday, req.OpenRules.DefaultOpenTimesForWeek.Monday.StartTime, req.OpenRules.DefaultOpenTimesForWeek.Monday.EndTime);
            room.OpenRules.ChangeDefaultOpenTimeForWeekDay(DayOfWeek.Tuesday, req.OpenRules.DefaultOpenTimesForWeek.Tuesday.StartTime, req.OpenRules.DefaultOpenTimesForWeek.Tuesday.EndTime);
            room.OpenRules.ChangeDefaultOpenTimeForWeekDay(DayOfWeek.Wednesday, req.OpenRules.DefaultOpenTimesForWeek.Wednesday.StartTime, req.OpenRules.DefaultOpenTimesForWeek.Wednesday.EndTime);
            room.OpenRules.ChangeDefaultOpenTimeForWeekDay(DayOfWeek.Thursday, req.OpenRules.DefaultOpenTimesForWeek.Thursday.StartTime, req.OpenRules.DefaultOpenTimesForWeek.Thursday.EndTime);
            room.OpenRules.ChangeDefaultOpenTimeForWeekDay(DayOfWeek.Friday, req.OpenRules.DefaultOpenTimesForWeek.Friday.StartTime, req.OpenRules.DefaultOpenTimesForWeek.Friday.EndTime);
            room.OpenRules.ChangeDefaultOpenTimeForWeekDay(DayOfWeek.Saturday, req.OpenRules.DefaultOpenTimesForWeek.Saturday.StartTime, req.OpenRules.DefaultOpenTimesForWeek.Saturday.EndTime);
            room.OpenRules.ChangeDefaultOpenTimeForWeekDay(DayOfWeek.Sunday, req.OpenRules.DefaultOpenTimesForWeek.Sunday.StartTime, req.OpenRules.DefaultOpenTimesForWeek.Sunday.EndTime);

            
            foreach (var openRulesOpenTimesSingleDay in req.OpenRules.OpenTimesSingleDays)
            {
                room.OpenRules.AddOrChangeExceptionsToWeekDayRules(openRulesOpenTimesSingleDay.Day.Date, openRulesOpenTimesSingleDay.TimeSlotDto.StartTime, openRulesOpenTimesSingleDay.TimeSlotDto.EndTime);
            }
            
            room.OpenRules.ChangeDefaultOpenAndStartDates(req.OpenRules.DefaultOpenDate, req.OpenRules.DefaultCloseDate);
            
            var reservations = await unitOfWork.Reservations.GetByRoomAsync(room.Id,ct);
            var deletableReservations = new List<Reservation>();

            foreach (var reservation in reservations)
            {
                if (reservation.IsConflicting(
                        room.OpenRules.DefaultOpenTimesForWeek,
                        room.OpenRules.ExceptionsToWeekDayRules,
                        room.OpenRules.DefaultOpenDate,
                        room.OpenRules.DefaultCloseDate))
                {
                    deletableReservations.Add(reservation);
                }
            }
            
            if (deletableReservations.Count != 0)
            {
                await unitOfWork.Reservations.DeleteManyAsync(deletableReservations.Select(x => x.Id), ct);
                
                await unitOfWork.CommitTransactionAsync(ct);
                
                sendEMailToUser.SendEMailToUsers(
                    deletableReservations.Select(x => x.UserId).ToList(),
                    $"Yor reservations for device in room: {room.Name} have been deleted.", 
                    "You have reservations that have been deleted because a device was removed by an administrator.");
                
                return TypedResults.Ok();
            }
            else
            {
                await unitOfWork.CommitTransactionAsync(ct);
                return TypedResults.Ok();
            }
        }
        finally
        {
            unitOfWork.Dispose();
        }
    }
}