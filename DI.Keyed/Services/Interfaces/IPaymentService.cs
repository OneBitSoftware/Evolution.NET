namespace DI.Keyed.Services.Interfaces;

public interface IPaymentService
{
    Task<Guid> ProcessPaymentAsync(string user, string paymentDetails);
}