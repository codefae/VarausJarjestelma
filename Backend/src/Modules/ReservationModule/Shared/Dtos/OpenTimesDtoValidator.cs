using FastEndpoints;
using FluentValidation.Results;

namespace src.Modules.ReservationModule.Shared.Dtos;

public class OpenTimesDtoValidator : Validator<OpenTimesDto>
{
    // TODO implement this
    public override ValidationResult Validate(FluentValidation.ValidationContext<OpenTimesDto> context)
    {
        throw new NotImplementedException();
    }
}