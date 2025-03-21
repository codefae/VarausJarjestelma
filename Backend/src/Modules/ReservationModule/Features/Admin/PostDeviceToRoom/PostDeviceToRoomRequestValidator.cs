using FastEndpoints;
using FluentValidation;

namespace src.Modules.ReservationModule.Features.PostDeviceToRoom;

public class PostDeviceToRoomRequestValidator : Validator<PostDeviceToRoomRequest>
{
    public PostDeviceToRoomRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .Must(BeAValidGuid).WithMessage("UserId must be a valid GUID.");

        RuleFor(x => x.DeviceDto.RoomId)
            .NotEmpty().WithMessage("RoomId is required.")
            .Must(BeAValidGuid).WithMessage("RoomId must be a valid GUID.");

        RuleFor(x => x.DeviceDto.DeviceId)
            .NotEmpty().WithMessage("DeviceId is required.")
            .Must(BeAValidGuid).WithMessage("DeviceId must be a valid GUID.");

        RuleFor(x => x.DeviceDto.DeviceName)
            .NotEmpty().WithMessage("DeviceName is required.");
    }

    private bool BeAValidGuid(string value)
    {
        return Guid.TryParse(value, out _);
    }
}