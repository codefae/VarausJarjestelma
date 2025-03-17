using FluentValidation;

namespace src.Modules.ReservationModule.Features.Admin.PostRoom;

public class PostRoomValidator : AbstractValidator<PostRoomRequest>
{
    public PostRoomValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Room name is required.");
    }
}