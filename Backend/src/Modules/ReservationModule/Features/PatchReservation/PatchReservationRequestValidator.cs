using System.Runtime.InteropServices.JavaScript;
using FastEndpoints;
using FluentValidation;


namespace src.Modules.ReservationModule.Features.PatchReservation;

public class PatchReservationRequestValidator : Validator<PatchReservationRequest>
{
    public PatchReservationRequestValidator()
    {
        RuleFor(x => x.ReservationId)
            .NotEmpty().WithMessage("Reservation ID is required.");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("Start time is required.");
        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("End time is required.")
            .GreaterThan(x => x.StartTime).WithMessage("End date must be after the start date.");

        RuleFor(x => x.Day)
            .NotEmpty().WithMessage("Day is required.")
            .GreaterThan(DateTime.Now - TimeSpan.FromDays(1)).WithMessage("Day must be in the future.");
    }
}