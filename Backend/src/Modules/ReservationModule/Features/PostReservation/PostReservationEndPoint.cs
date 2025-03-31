using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Domain.DomainServices.Interfaces;
using src.Modules.ReservationModule.Domain.DomainServices.ResultEnums;
using src.Modules.ReservationModule.Shared.Interfaces;
using src.Modules.ReservationModule.Shared.EndPointGroups;

namespace src.Modules.ReservationModule.Features.PostReservation;

public class PostReservationEndPoint(
    IBookingDomainService bookingDomainService,
    IUnitOfWork unitOfWork)
    : Endpoint<PostReservationRequest, 
        Results<NotFound<string>, Ok<string>, ProblemHttpResult>,
        PostReservationMapper>
{
    public override void Configure()
    {
        Post("reservations");
        Validator<PostReservationRequestValidator>();
        AllowAnonymous();
        Group<UserEndpointGroup>();
    }

    public override async Task<Results<NotFound<string>, Ok<string>, ProblemHttpResult>> ExecuteAsync(
        PostReservationRequest req, CancellationToken ct)
    {
        var reservation = Map.ToEntity(req);

        try
        {
            await unitOfWork.BeginTransactionAsync(ct);

            var roomTask = unitOfWork.Rooms.GetAsync(reservation.RoomId, ct);
            var reservationsTask = unitOfWork.Reservations.GetByRoomAndDateAsync(
                reservation.RoomId,
                reservation.Day,
                ct);

            await Task.WhenAll(roomTask, reservationsTask);

            var room = await roomTask;
            var reservations = await reservationsTask;

            if (room == null)
                return TypedResults.NotFound("Room not found!");

            var result = bookingDomainService.ValidateReservation(reservation, room, reservations);

            switch (result)
            {
                case ValidateReservationResult.Success:
                    await unitOfWork.Reservations.AddAsync(reservation, ct);
                    await unitOfWork.CommitTransactionAsync(ct);
                    return TypedResults.Ok("Reservation created successfully.");
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
        finally
        {
            unitOfWork.Dispose();
        }
    }
}