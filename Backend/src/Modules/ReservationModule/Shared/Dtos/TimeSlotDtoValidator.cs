using FastEndpoints;
using FluentValidation;
using FluentValidation.Results;

namespace src.Modules.ReservationModule.Shared.Dtos;

public class TimeSlotDtoValidator : Validator<TimeSlotDto>
{
    public TimeSlotDtoValidator()
    {
        RuleFor(x => x.StartTime)
            .LessThan(x => x.EndTime)
            .WithMessage("StartTime must be earlier than EndTime");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("EndTime must be later than StartTime");
    }
} 
