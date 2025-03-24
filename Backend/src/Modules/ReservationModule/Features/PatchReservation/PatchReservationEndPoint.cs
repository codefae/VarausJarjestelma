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
        Results<Ok<string>, NotFound<string>, ProblemHttpResult>>
{
    public override void Configure()
    {
        Patch("/reservations");
        Validator<PatchReservationRequestValidator>();
        AllowAnonymous();
    }

    public override async Task<Results<Ok<string>, NotFound<string>, ProblemHttpResult>> ExecuteAsync(
        PatchReservationRequest req, CancellationToken ct)
    {
        if (!Guid.TryParse(req.ReservationId, out var reservationId))
            throw new ArgumentException("Invalid reservation id format!");
        
        await unitOfWork.BeginTransactionAsync(ct);

        var reservation = await unitOfWork.Reservations.GetAsync(reservationId, ct);
        if (reservation == null)
            return TypedResults.NotFound("Reservation not found!");
        

        var room = await unitOfWork.Rooms.GetAsync(reservation.RoomId, ct);
        if (room == null)
            return TypedResults.NotFound("Room not found!");
        

        var reservations =
            (await unitOfWork.Reservations.GetByRoomAndDateAsync(room.Id, req.Day, ct))
            .Where(x => x.Id != reservation.Id);

        reservation.ChangeReservationTime(req.Day, req.TimeSlotDto.StartTime, req.TimeSlotDto.EndTime);
        var result = bookingDomainService.ValidateReservation(reservation, room, reservations);

        switch (result)
        {
            case ValidateReservationResult.Success:
                await unitOfWork.Reservations.UpdateAsync(reservation, ct);
                await unitOfWork.CommitTransactionAsync(ct);
                return TypedResults.Ok("Reservation updated successfully.");
            case ValidateReservationResult.ReservationConflicts:
                await unitOfWork.RollbackTransactionAsync(ct);
                return TypedResults.Problem("Reservation conflicts with other reservations!");
            case ValidateReservationResult.DeviceNotFound:
                await unitOfWork.RollbackTransactionAsync(ct);
                return TypedResults.NotFound("Device not found!");
            case ValidateReservationResult.RoomNotOpen:
                await unitOfWork.RollbackTransactionAsync(ct);
                return TypedResults.Problem("Room is not open!");
            default:
                await unitOfWork.RollbackTransactionAsync(ct);
                throw new ArgumentOutOfRangeException();
        }
    }
}