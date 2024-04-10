namespace DI.Keyed.Services;

using DI.Keyed.Services.Interfaces;

public class EmailNotificationService : INotificationService
{
    public Task<Guid> SendAsync(string recipient, string message)
    {
        Console.WriteLine($"Sending e-mail notification to {recipient}... Message: {message}");
        return Task.FromResult(Guid.NewGuid());
    }
}