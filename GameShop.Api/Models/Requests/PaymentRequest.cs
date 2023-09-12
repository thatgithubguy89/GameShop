namespace GameShop.Api.Models.Requests
{
    public class PaymentRequest
    {
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public decimal? Total { get; set; }
        public string? CardNumber { get; set; }
        public string? ExpirationYear { get; set; }
        public string? ExpirationMonth { get; set; }
        public string? Cvc { get; set; }
    }
}
