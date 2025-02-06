namespace src.Modules.ReservationModule.Domain.DomainServices.ResultEnums;

public enum ValidateReservationResult
{
    Success,
    DeviceNotFound,
    RoomNotOpen,
    ReservationConflicts
}