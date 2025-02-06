using FastEndpoints;
using FluentValidation.Results;

namespace src.Modules.ReservationModule.Shared.Dtos;

public class ReservationDtoValidator : Validator<ReservationDto>
{
    // TODO implement this
    public override ValidationResult Validate(FluentValidation.ValidationContext<ReservationDto> context)
    {
        throw new NotImplementedException();
    }
}