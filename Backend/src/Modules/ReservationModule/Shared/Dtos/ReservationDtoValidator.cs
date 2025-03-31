using FastEndpoints;
using FluentValidation;
using FluentValidation.Results;

namespace src.Modules.ReservationModule.Shared.Dtos;

public class ReservationDtoValidator : Validator<ReservationDto>
{
    public ReservationDtoValidator()
    {
        
        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("RoomId is required.");
        
        RuleFor(x => x.ReservationType)
            .NotEmpty().WithMessage("ReservationType is required.");
        
        RuleFor(x => x.TimeSlotDto.StartTime)
            .LessThan(x => x.TimeSlotDto.EndTime)
            .WithMessage("StartTime must be earlier than EndTime.");
        
        
        RuleFor(x => x.DeviceId)
            .Must(id => string.IsNullOrEmpty(id) || Guid.TryParse(id, out _))
            .WithMessage("DeviceId must be a valid GUID or null.");
    }
}