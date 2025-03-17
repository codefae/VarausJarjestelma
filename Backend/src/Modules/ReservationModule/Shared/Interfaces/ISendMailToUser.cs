namespace src.Modules.ReservationModule.Shared.Interfaces;

public interface ISendEMailToUser
{
    bool SendEMailToUsers(List<Guid> userIds, string subject, string body);
}