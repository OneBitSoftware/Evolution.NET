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

        var allNotificationServices = scope.ServiceProvider.GetServices<INotificationService>().ToArray();
        Console.WriteLine($"There are {allNotificationServices.Length} registered non-keyed services: {string.Join(", ", allNotificationServices.Select(x => TypeNames.Get(x.GetType())))}");

        var allKeyedNotificationServices = scope.ServiceProvider.GetKeyedServices<INotificationService>("_default");
        Console.WriteLine($"There are {allNotificationServices.Length} registered services for the \"_default\" key: {string.Join(", ", allNotificationServices.Select(x => TypeNames.Get(x.GetType())))}");

        // The following line will always resolve the last registered service of type INotificationService..
        var defaultNotificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
        Console.WriteLine($"The type of the default non-keyed notification service is: {TypeNames.Get(defaultNotificationService.GetType())}");
        await defaultNotificationService.SendAsync("Tony Troeff", "Hello, world!");

        Console.Write("Enter the type of notification to send: ");
        var notificationType = Console.ReadLine()!;

        var requestedNotificationService = scope.ServiceProvider.GetRequiredKeyedService<INotificationService>(notificationType);
        Console.WriteLine($"The type of the requested notification service is: {TypeNames.Get(requestedNotificationService.GetType())}");
        await requestedNotificationService.SendAsync("Tony Troeff", "Hello, world!");
    }

    private static ServiceProvider BuildServices()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<INotificationService, SmsNotificationService>();
        serviceCollection.AddScoped<INotificationService, EmailNotificationService>();

        serviceCollection.AddKeyedScoped<INotificationService, SmsNotificationService>("_default");
        serviceCollection.AddKeyedScoped<INotificationService, EmailNotificationService>("_default");

        serviceCollection.AddKeyedScoped<INotificationService, SmsNotificationService>("sms");
        serviceCollection.AddKeyedScoped<INotificationService, EmailNotificationService>("email");

        var buildOptions = new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true };
        return serviceCollection.BuildServiceProvider(buildOptions);
    }
}