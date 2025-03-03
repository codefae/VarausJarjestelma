using FluentValidation;
using FluentValidation.Results;

namespace src.Modules.ReservationModule.Shared.Dtos;

public class DeviceDtoValidator : AbstractValidator<DeviceDto>
{
    public DeviceDtoValidator()
    {
        RuleFor(device => device.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(device => device.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(device => device.DeviceType)
            .NotEmpty().WithMessage("DeviceType is required.");

        RuleFor(device => device.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
}
