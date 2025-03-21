using FastEndpoints;
using FluentValidation;

namespace src.Modules.ReservationModule.Features.Admin.PostDeviceToRoom;

public class PostDeviceToRoomRequestValidator : Validator<PostDeviceToRoomRequest>
{
    public PostDeviceToRoomRequestValidator()
    {
        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("RoomId is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.DeviceType)
            .NotEmpty().WithMessage("DeviceType is required.");

        RuleFor(x => x.Description) 
            .NotEmpty().WithMessage("Description is required.");
    }
}