using FastEndpoints;
using FluentValidation.Results;

namespace src.Modules.ReservationModule.Features.PatchReservationStartAndEndTimes;

public class PatchReservationStartAndEndTimesValidator : Validator<PatchReservationStartAndEndTimesRequest>
{
    public override ValidationResult Validate(FluentValidation.ValidationContext<PatchReservationStartAndEndTimesRequest> context)
    {
        throw new NotImplementedException();
    }
}