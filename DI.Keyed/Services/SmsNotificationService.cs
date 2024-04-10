namespace DI.Keyed.Services;

using DI.Keyed.Services.Interfaces;
using TryAtSoftware.Extensions.DependencyInjection.Attributes;
using TryAtSoftware.Extensions.DependencyInjection.Standard.Attributes;

[AutomaticallyRegisteredService, ServiceConfiguration(Key = "sms")]
public class SmsNotificationService : INotificationService
{
    public Task<Guid> SendAsync(string recipient, string message)
    {
        Console.WriteLine($"Sending SMS notification to {recipient}... Message: {message}");
        return Task.FromResult(Guid.NewGuid());
    }
}