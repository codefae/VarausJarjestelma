using FastEndpoints;
using FluentValidation;
using FluentValidation.Results;

namespace src.Modules.ReservationModule.Shared.Dtos;

public class DeviceDtoValidator : Validator<DeviceDto>
{
    // TODO implement this
    public override ValidationResult Validate(FluentValidation.ValidationContext<DeviceDto> context)
    {
        throw new NotImplementedException();
    }
}