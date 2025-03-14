using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using src.Modules.ReservationModule.Domain.DomainServices.Interfaces;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Domain.DomainServices.ResultEnums;
using src.Modules.ReservationModule.Shared.Interfaces;


namespace src.Modules.ReservationModule.Features.PatchReservation;

public class PatchReservationEndPoint(
    IReservationRepository reservationRepository,
    IRoomRepository roomRepository,
    IBookingDomainService bookingDomainService,
    ILogger<PatchReservationEndPoint> logger)
    : EndpointWithMapper<PatchReservationTimeRequest, PatchReservationMapper>
{
    public override void Configure()
    {
        Patch("/reservations/{id}");
        Validator<PatchReservationTimeRequestValidator>();
        AllowAnonymous();
    }

    public override async Task<Results<NotFound<string>, Ok<string>, ProblemHttpResult>> HandleAsync(PatchReservationTimeRequest req, CancellationToken ct)
    {
        var reservationId = req.Id;
        var reservation = await reservationRepository.GetReservationByIdAsync(reservationId);
        if (reservation == null)
        {
            return TypedResults.NotFound("Reservation not found!");
        }

        var room = await roomRepository.GetRoomByIdAsync(reservation.RoomId);
        if (room == null)
        {
            return TypedResults.NotFound("Room not found!");
        }

        // Business Logic
        var result = bookingDomainService.ValidateReservation(reservation, room, new List<Reservation>());

        // Io logic
        switch (result)
        {
            case ValidateReservationResult.Success:
                try
                {
                    await reservationRepository.UpdateReservationAsync(reservation);
                    return TypedResults.Ok("Reservation updated successfully.");
                }
                catch (DbUpdateConcurrencyException e)
                {
                    logger.LogInformation("Concurrency exception occurred while updating the reservation, retrying...");
                }
                break;
            case ValidateReservationResult.ReservationConflicts:
                return TypedResults.Problem("Reservation conflicts with other reservations!");
            case ValidateReservationResult.DeviceNotFound:
                return TypedResults.NotFound("Device not found!");
            case ValidateReservationResult.RoomNotOpen:
                return TypedResults.Problem("Room is not open!");
        }

        return TypedResults.Problem("Many people try to update reservations at the same time, try again later!");
    }
}