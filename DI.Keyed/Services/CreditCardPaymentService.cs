namespace DI.Keyed.Services;

using DI.Keyed.Services.Interfaces;
using TryAtSoftware.Extensions.DependencyInjection.Attributes;
using TryAtSoftware.Extensions.DependencyInjection.Standard.Attributes;

[AutomaticallyRegisteredService, ServiceConfiguration(Key = "credit_card")]
public class CreditCardPaymentService : IPaymentService
{
    public Task<Guid> ProcessPaymentAsync(string user, string paymentDetails)
    {
        Console.WriteLine($"Processing payments using credit card for user \"{user}\". Details: {paymentDetails}");
        return Task.FromResult(Guid.NewGuid());
    }
}