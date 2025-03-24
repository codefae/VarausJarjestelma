using FastEndpoints;
using FluentValidation;

namespace src.Modules.ReservationModule.Features.Admin.PostDeviceToRoom;

public class PostDeviceToRoomRequestValidator : Validator<PostDeviceToRoomRequest>
{
    public PostDeviceToRoomRequestValidator()
    {
        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("RoomId is required.")
            .Must(x => Guid.TryParse(x, out _)).WithMessage("RoomId must be a valid GUID.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(1, 100).WithMessage("Name must be between 1 and 100 characters.");
        
        RuleFor(x => x.DeviceType)
            .NotEmpty().WithMessage("DeviceType is required.")
            .Length(1, 100).WithMessage("DeviceType must be between 1 and 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(1, 100).WithMessage("Name must be between 1 and 100 characters.");
    }
}