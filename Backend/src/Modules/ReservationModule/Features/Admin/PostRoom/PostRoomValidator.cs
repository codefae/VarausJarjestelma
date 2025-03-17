using FastEndpoints;
using FluentValidation;
using FluentValidation.Results;

namespace src.Modules.ReservationModule.Features.Admin.PostRoom;

public class PostRoomValidator : AbstractValidator<PostRoomRequest>
{
    public PostRoomValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Room name is required.");
        RuleFor(x => x.DefaultOpenDate).NotEmpty().WithMessage("Default open date is required.");
        RuleFor(x => x.DefaultCloseDate).NotEmpty().WithMessage("Default close date is required.");
    }
}