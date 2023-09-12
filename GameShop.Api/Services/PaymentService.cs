using GameShop.Api.Interfaces;
using GameShop.Api.Models.Requests;
using GameShop.Api.Models.Stripe;
using Stripe;

namespace GameShop.Api.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ChargeService _chargeService;
        private readonly CustomerService _customerService;
        private readonly TokenService _tokenService;

        public PaymentService(ChargeService chargeService, CustomerService customerService, TokenService tokenService)
        {
            _chargeService = chargeService;
            _customerService = customerService;
            _tokenService = tokenService;
        }

        public async Task ProcessPayment(PaymentRequest paymentRequest)
        {
            var customer = await AddStripeCustomer(paymentRequest);

            var paymentOptions = new ChargeCreateOptions
            {
                Customer = customer.CustomerId,
                ReceiptEmail = paymentRequest.Email,
                Description = "Gameshop Payment",
                Currency = "USD",
                Amount = (long)Math.Ceiling(paymentRequest.Total ?? 0) * 100
            };

            await _chargeService.CreateAsync(paymentOptions);
        }

        private async Task<StripeCustomer> AddStripeCustomer(PaymentRequest paymentRequest)
        {
            var tokenOptions = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Name = paymentRequest.FullName,
                    Number = paymentRequest.CardNumber,
                    ExpYear = paymentRequest.ExpirationYear,
                    ExpMonth = paymentRequest.ExpirationMonth,
                    Cvc = paymentRequest.Cvc
                }
            };

            var token = await _tokenService.CreateAsync(tokenOptions, null);

            var customerCreateOptions = new CustomerCreateOptions
            {
                Name = paymentRequest.FullName,
                Email = paymentRequest.Email,
                Source = token.Id
            };

            var createdCustomer = await _customerService.CreateAsync(customerCreateOptions);

            return new StripeCustomer { Name = createdCustomer.Name, Email = createdCustomer.Email, CustomerId = createdCustomer.Id };
        }
    }
}
