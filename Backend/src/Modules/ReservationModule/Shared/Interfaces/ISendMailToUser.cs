namespace src.Modules.ReservationModule.Shared.Interfaces;

public interface ISendEMailToUser
{
    void SendEMailToUsers(List<Guid> userIds, string subject, string body);
}