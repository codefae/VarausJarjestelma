using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using src.Modules.ReservationModule.Domain.DomainServices.Interfaces;
using src.Modules.ReservationModule.Domain.DomainServices.ResultEnums;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Features.PostReservation;

public class PostReservationEndPoint(
    IReservationRepository reservationRepository,
    IRoomRepository roomRepository,
    IBookingDomainService bookingDomainService,
    ILogger<PostReservationEndPoint> logger)
    : EndpointWithMapper<PostReservationRequest, PostReservationMapper>
{
    public override void Configure()
    {
        Post("/reservations");
        Validator<PostReservationRequestValidator>();
        AllowAnonymous();
    }

    public override async Task<Results<NotFound<string>,Ok<string>,ProblemHttpResult>> HandleAsync(PostReservationRequest req, CancellationToken ct)
    {
        var reservation = Map.ToEntity(req);
        
        var retries = 5;
        while(retries-- > 0)
        {
            // Io logic
            var roomTask = roomRepository.GetRoomByIdAsync(reservation.RoomId);
            var reservationsTask = reservationRepository.GetByRoomAndDateAsync(
                reservation.RoomId,
                reservation.Day);
            
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
                    try
                    {
                        await reservationRepository.AddAndMakeSureRoomIsNotChangedAsync(reservation);
                        return TypedResults.Ok("Reservation created successfully.");
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        logger.LogInformation("Concurrency exception occurred while creating a new reservation, retrying...");
                    }
                    break;
                case ValidateReservationResult.ReservationConflicts:
                    return TypedResults.Problem("Reservation conflicts with other reservations!");
                case ValidateReservationResult.DeviceNotFound:
                    return TypedResults.NotFound("Device not found!");
                case ValidateReservationResult.RoomNotOpen:
                    return TypedResults.Problem("Room is not open!");
            }
        }
        
        return TypedResults.Problem("Many people try to create reservations a the same time try again later!");
    }
}