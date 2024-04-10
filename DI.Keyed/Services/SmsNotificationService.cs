namespace DI.Keyed.Services;

using DI.Keyed.Services.Interfaces;

public class SmsNotificationService : INotificationService
{
    public Task<Guid> SendAsync(string recipient, string message)
    {
        Console.WriteLine($"Sending SMS notification to {recipient}... Message: {message}");
        return Task.FromResult(Guid.NewGuid());
    }
}