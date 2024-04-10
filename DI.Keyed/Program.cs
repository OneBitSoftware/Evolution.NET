namespace DI.Keyed;

using System.Reflection;
using DI.Keyed.Services;
using DI.Keyed.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TryAtSoftware.Extensions.DependencyInjection;
using TryAtSoftware.Extensions.DependencyInjection.Interfaces;
using TryAtSoftware.Extensions.DependencyInjection.Standard;
using TryAtSoftware.Extensions.Reflection;

public static class Program
{
    public static async Task Main()
    {
        await using var rootServiceProvider = BuildServices();

        using var scope = rootServiceProvider.CreateScope();

        var allPaymentServices = scope.ServiceProvider.GetServices<IPaymentService>().ToArray();
        Console.WriteLine($"There are {allPaymentServices.Length} registered non-keyed services.");
        foreach (var service in allPaymentServices) Console.WriteLine(TypeNames.Get(service.GetType()));

        var allKeyedPaymentServices = scope.ServiceProvider.GetKeyedServices<IPaymentService>("_default").ToArray();
        Console.WriteLine($"There are {allKeyedPaymentServices.Length} registered services for the \"_default\" key.");
        foreach (var service in allKeyedPaymentServices) Console.WriteLine(TypeNames.Get(service.GetType()));

        if (allPaymentServices.Length > 0)
        {
            // The following line will always resolve the last registered service of type INotificationService..
            var defaultPaymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
            Console.WriteLine($"The type of the default non-keyed payment service is: {TypeNames.Get(defaultPaymentService.GetType())}");
            await defaultPaymentService.ProcessPaymentAsync("Tony Troeff", "Hello, world!");
        }

        Console.Write("Enter the key of the requested payment service: ");
        var paymentServiceKey = Console.ReadLine()!;

        var requestedPaymentService = scope.ServiceProvider.GetRequiredKeyedService<IPaymentService>(paymentServiceKey);
        Console.WriteLine($"The type of the requested payment service is: {TypeNames.Get(requestedPaymentService.GetType())}");
        await requestedPaymentService.ProcessPaymentAsync("Tony Troeff", "Hello, world!");
    }

    private static ServiceProvider BuildServices()
    {
        var serviceCollection = new ServiceCollection();
        // serviceCollection.AddScoped<IPaymentService, ApplePayPaymentService>();
        // serviceCollection.AddScoped<IPaymentService, CreditCardPaymentService>();
        // serviceCollection.AddScoped<IPaymentService, GooglePayPaymentService>();

        serviceCollection.AddKeyedScoped<IPaymentService, ApplePayPaymentService>("_default");
        serviceCollection.AddKeyedScoped<IPaymentService, CreditCardPaymentService>("_default");
        serviceCollection.AddKeyedScoped<IPaymentService, GooglePayPaymentService>("_default");

        serviceCollection.AddKeyedScoped<IPaymentService, ApplePayPaymentService>("apple_pay");
        serviceCollection.AddKeyedScoped<IPaymentService, CreditCardPaymentService>("credit_card");
        serviceCollection.AddKeyedScoped<IPaymentService, GooglePayPaymentService>("google_pay");

        // IServiceRegistrar registrar = new ServiceRegistrar(serviceCollection, new HierarchyScanner());
        // Assembly[] allAssemblies = [Assembly.GetExecutingAssembly()];
        // allAssemblies.AutoRegisterServices(registrar);

        var buildOptions = new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true };
        return serviceCollection.BuildServiceProvider(buildOptions);
    }
}