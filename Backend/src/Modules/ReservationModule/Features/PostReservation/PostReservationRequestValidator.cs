using FastEndpoints;
using FluentValidation;
using src.Modules.ReservationModule.Shared.Dtos;

namespace src.Modules.ReservationModule.Features.PostReservation;

public class PostReservationRequestValidator : Validator<PostReservationRequest>
{
    public PostReservationRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.ReservationDto)
            .SetValidator(new ReservationDtoValidator());
    }
}