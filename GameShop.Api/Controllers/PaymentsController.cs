using GameShop.Api.Interfaces;
using GameShop.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost("createpayment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreatePayment(PaymentRequest paymentRequest)
        {
            try
            {
                _logger.LogInformation("Creating payment.");

                if (paymentRequest == null)
                    return BadRequest();

                await _paymentService.ProcessPayment(paymentRequest);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to created payment: {}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
