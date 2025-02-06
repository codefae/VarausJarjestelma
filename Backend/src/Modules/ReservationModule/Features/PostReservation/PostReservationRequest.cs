using src.Modules.ReservationModule.Shared.Dtos;

namespace src.Modules.ReservationModule.Features.PostReservation;

public class PostReservationRequest
{
    public required string UserId { get; init; }
    public required ReservationDto ReservationDto { get; init; }
}