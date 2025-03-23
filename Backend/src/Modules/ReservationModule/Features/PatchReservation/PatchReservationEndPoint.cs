using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using src.Modules.ReservationModule.Domain.DomainServices.Interfaces;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Domain.DomainServices.ResultEnums;
using src.Modules.ReservationModule.Shared.Interfaces;


namespace src.Modules.ReservationModule.Features.PatchReservation;

public class PatchReservationEndPoint(
    IBookingDomainService bookingDomainService,
    IUnitOfWork unitOfWork,
    ILogger<PatchReservationEndPoint> logger)
    : Endpoint<
        PatchReservationRequest,
        Task<Results<Ok<string>, NotFound<string>, ProblemHttpResult>>>
{
    public override void Configure()
    {
        Patch("/reservations");
        Validator<PatchReservationRequestValidator>();
        AllowAnonymous();
    }

    public override async Task<Results<Ok<string>, NotFound<string>, ProblemHttpResult>> HandleAsync(
        PatchReservationRequest req, CancellationToken ct)
    {
        var reservationId = Guid.Parse(req.ReservationId);

        await unitOfWork.BeginTransactionAsync();

        var reservation = await unitOfWork.Reservations.GetAsync(reservationId, ct);
        if (reservation == null)
        {
            return TypedResults.NotFound("Reservation not found!");
        }

        var room = await unitOfWork.Rooms.GetRoomByIdAsync(reservation.RoomId, ct);
        if (room == null)
        {
            return TypedResults.NotFound("Room not found!");
        }

        var reservations =
            (await unitOfWork.Reservations.GetByRoomAsync(room.Id, ct))
            .Where(x => x.Id != reservation.Id);

        reservation.ChangeReservationTime(req.Day, req.TimeSlotDto.StartTime, req.TimeSlotDto.EndTime);
        var result = bookingDomainService.ValidateReservation(reservation, room, reservations);

        switch (result)
        {
            case ValidateReservationResult.Success:
                await unitOfWork.Reservations.UpdateAsync(reservation, ct);
                return TypedResults.Ok("Reservation updated successfully.");
            case ValidateReservationResult.ReservationConflicts:
                return TypedResults.Problem("Reservation conflicts with other reservations!");
            case ValidateReservationResult.DeviceNotFound:
                return TypedResults.NotFound("Device not found!");
            case ValidateReservationResult.RoomNotOpen:
                return TypedResults.Problem("Room is not open!");
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}