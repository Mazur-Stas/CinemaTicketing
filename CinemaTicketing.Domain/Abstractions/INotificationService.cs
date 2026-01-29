namespace CinemaTicketing.Domain.Abstractions;

public interface INotificationService
{
    Task Notify(string email,string subject,string message);
}