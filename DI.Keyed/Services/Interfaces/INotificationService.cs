namespace DI.Keyed.Services.Interfaces;

public interface INotificationService
{
    Task<Guid> SendAsync(string recipient, string message);
}