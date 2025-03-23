using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using src.Modules.ReservationModule.Domain.DomainServices.Interfaces;
using src.Modules.ReservationModule.Domain.DomainServices.ResultEnums;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Features.PostReservation;

public class PostReservationEndPoint(
    IBookingDomainService bookingDomainService,
    IUnitOfWork unitOfWork,
    ILogger<PostReservationEndPoint> logger)
    : EndpointWithMapper<PostReservationRequest, PostReservationMapper>
{
    public override void Configure()
    {
        Post("/reservations");
        Validator<PostReservationRequestValidator>();
        AllowAnonymous();
    }

    public override async Task<Results<NotFound<string>, Ok<string>, ProblemHttpResult>> HandleAsync(
        PostReservationRequest req, CancellationToken ct)
    {
        var reservation = Map.ToEntity(req);
        await unitOfWork.BeginTransactionAsync();

        // Io logic
        var roomTask = unitOfWork.Rooms.GetRoomByIdAsync(reservation.RoomId, ct);
        var reservationsTask = unitOfWork.Reservations.GetByRoomAndDateAsync(
            reservation.RoomId,
            reservation.Day,
            ct);

        await Task.WhenAll(roomTask, reservationsTask);

        var room = await roomTask;
        var reservations = await reservationsTask;

        if (room == null)
        {
            return TypedResults.NotFound("Room not found!");
        }

        // Business Logc
        var result = bookingDomainService.ValidateReservation(reservation, room, reservations);

        // Io logic
        switch (result)
        {
            case ValidateReservationResult.Success:
                await unitOfWork.Reservations.AddAsync(reservation, ct);
                await unitOfWork.CommitTransactionAsync();
                return TypedResults.Ok("Reservation created successfully.");
            case ValidateReservationResult.ReservationConflicts:
                await unitOfWork.RollbackTransactionAsync();
                return TypedResults.Problem("Reservation conflicts with other reservations!");
            case ValidateReservationResult.DeviceNotFound:
                await unitOfWork.RollbackTransactionAsync();
                return TypedResults.NotFound("Device not found!");
            case ValidateReservationResult.RoomNotOpen:
                await unitOfWork.RollbackTransactionAsync();
                return TypedResults.Problem("Room is not open!");
            default:
                await unitOfWork.RollbackTransactionAsync();
                throw new ArgumentOutOfRangeException();
        }
    }
}