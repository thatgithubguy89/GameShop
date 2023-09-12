using GameShop.Api.Models.Requests;

namespace GameShop.Api.Interfaces
{
    public interface IPaymentService
    {
        Task ProcessPayment(PaymentRequest paymentRequest);
    }
}
