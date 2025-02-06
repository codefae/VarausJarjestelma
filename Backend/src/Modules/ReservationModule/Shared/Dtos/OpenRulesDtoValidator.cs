using FastEndpoints;
using FluentValidation.Results;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

namespace src.Modules.ReservationModule.Shared.Dtos;

public class OpenRulesDtoValidator : Validator<OpenRulesDto>
{
    // TODO implement
    public override ValidationResult Validate(FluentValidation.ValidationContext<OpenRulesDto> context)
    {
        throw new NotImplementedException();
    }
}