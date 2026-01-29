using CinemaTicketing.Domain.Abstractions;

namespace CinemaTicketing.Infrastructure.Notifications;

public class NotificationService : INotificationService
{
    public Task Notify(string email, string subject, string message)
    {
        Console.WriteLine($"Sent to {email}: [{subject}]; {message}");
        return Task.CompletedTask;
    }
}