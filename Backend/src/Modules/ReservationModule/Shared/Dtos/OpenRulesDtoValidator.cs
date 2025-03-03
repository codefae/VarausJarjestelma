using FluentValidation;
using FluentValidation.Results;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

namespace src.Modules.ReservationModule.Shared.Dtos;

public class OpenRulesDtoValidator : AbstractValidator<OpenRulesDto>
{
    public OpenRulesDtoValidator()
    {
        RuleFor(rules => rules.DefaultOpenDate)
            .NotEmpty().WithMessage("DefaultOpenDate is required.")
            .LessThan(rules => rules.DefaultCloseDate).WithMessage("DefaultOpenDate must be before DefaultCloseDate.");

        RuleFor(rules => rules.DefaultCloseDate)
            .NotEmpty().WithMessage("DefaultCloseDate is required.");

        RuleFor(rules => rules.OpenTimesSingleDays)
            .NotNull().WithMessage("OpenTimesSingleDays is required.");

        RuleFor(rules => rules.DefaultOpenTimesForWeek)
            .NotNull().WithMessage("DefaultOpenTimesForWeek is required.");
    }
}
