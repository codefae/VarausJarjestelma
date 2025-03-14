using FastEndpoints;
using FluentValidation;


namespace src.Modules.ReservationModule.Features.PatchReservation;

public class PatchReservationTimeRequestValidator : Validator<PatchReservationTimeRequest>
{
    public PatchReservationTimeRequestValidator()
    {
        RuleFor(x => x.ReservationId)
            .NotEmpty().WithMessage("Reservation ID is required.")
            .GreaterThan(0).WithMessage("Reservation ID must be greater than 0.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.")
            .Must(BeAValidDate).WithMessage("Start date must be a valid date.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required.")
            .Must(BeAValidDate).WithMessage("End date must be a valid date.")
            .GreaterThan(x => x.StartDate).WithMessage("End date must be after the start date.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }

    private bool BeAValidDate(DateTime date)
    {
        return !date.Equals(default(DateTime));
    }
}