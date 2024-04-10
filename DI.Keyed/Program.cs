namespace DI.Keyed;

using DI.Keyed.Services;
using DI.Keyed.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TryAtSoftware.Extensions.Reflection;

public static class Program
{
    public static async Task Main()
    {
        await using var rootServiceProvider = BuildServices();

        using var scope = rootServiceProvider.CreateScope();

        // The following line will always resolve the last registered service of type INotificationService..
        var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
        Console.WriteLine($"The type of resolved notification service is: {TypeNames.Get(notificationService.GetType())}");
        await notificationService.SendAsync("Tony Troeff", "Hello, world!");
    }

    private static ServiceProvider BuildServices()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<INotificationService, SmsNotificationService>();
        serviceCollection.AddScoped<INotificationService, EmailNotificationService>();
        // serviceCollection.AddKeyedScoped<INotificationService, SmsNotificationService>("sms");
        // serviceCollection.AddKeyedScoped<INotificationService, EmailNotificationService>("email");

        var buildOptions = new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true };
        return serviceCollection.BuildServiceProvider(buildOptions);
    }
}