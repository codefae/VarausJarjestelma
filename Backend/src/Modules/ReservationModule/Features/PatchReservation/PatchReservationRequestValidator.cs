using System.Runtime.InteropServices.JavaScript;
using FastEndpoints;
using FluentValidation;
using src.Modules.ReservationModule.Shared.Dtos;


namespace src.Modules.ReservationModule.Features.PatchReservation;

public class PatchReservationRequestValidator : Validator<PatchReservationRequest>
{
    public PatchReservationRequestValidator()
    {
        RuleFor(x => x.ReservationId)
            .NotEmpty().WithMessage("Reservation ID is required.");

        RuleFor(x => x.Day)
            .NotEmpty().WithMessage("Day is required.");

        RuleFor(x => x.TimeSlotDto).SetValidator(new TimeSlotDtoValidator());

        RuleFor(x => x)
            .Must(IsResrevationInFuture).WithMessage("Reservation is not in the future.");
    }

    private bool IsResrevationInFuture(PatchReservationRequest request)
    {
        if (request.Day.Day > DateTime.Now.Day)
            return true;

        if (request.Day.Day == DateTime.Now.Day)
        {
            if(request.TimeSlotDto.StartTime > DateTime.Now.TimeOfDay)
                return true;
        }
        
       return false;
    }
}