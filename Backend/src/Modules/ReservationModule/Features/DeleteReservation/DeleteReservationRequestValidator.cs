using FastEndpoints;
using FluentValidation.Results;

namespace src.Modules.ReservationModule.Features.DeleteReservation;

public class DeleteReservationRequestValidator : Validator<DeleteReservationRequest>
{
    public override ValidationResult Validate(FluentValidation.ValidationContext<DeleteReservationRequest> context)
    {
        throw new NotImplementedException();
    }
}