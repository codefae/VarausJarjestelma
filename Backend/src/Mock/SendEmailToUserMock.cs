using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Mock;

public class SendEmailToUserMock : ISendEMailToUser
{
    public void SendEMailToUsers(List<Guid> userIds, string subject, string body)
    {
        Console.WriteLine("Simulating sending emails to users.");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Body: {body}");
        foreach (var userId in userIds)
        {
            Console.WriteLine($"Sending email to user {userId}");
        }
    }
}