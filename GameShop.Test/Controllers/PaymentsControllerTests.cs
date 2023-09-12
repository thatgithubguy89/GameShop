using GameShop.Api.Controllers;
using GameShop.Api.Interfaces;
using GameShop.Api.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace GameShop.Test.Controllers
{
    public class PaymentsControllerTests
    {
        Mock<IPaymentService> _mockPaymentService;
        Mock<ILogger<PaymentsController>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockPaymentService = new Mock<IPaymentService>();

            _mockLogger = new Mock<ILogger<PaymentsController>>();
        }

        [Test]
        public async Task CreatePayment()
        {
            _mockPaymentService.Setup(p => p.ProcessPayment(It.IsAny<PaymentRequest>()));
            var _paymentsController = new PaymentsController(_mockPaymentService.Object, _mockLogger.Object);

            var actionResult = await _paymentsController.CreatePayment(new PaymentRequest());
            var result = actionResult as NoContentResult;

            result.StatusCode.ShouldBe(StatusCodes.Status204NoContent);
        }

        [Test]
        public async Task CreatePayment_GivenInvalidPaymentRequest_ShouldReturn_BadRequest()
        {
            _mockPaymentService.Setup(p => p.ProcessPayment(It.IsAny<PaymentRequest>()));
            var _paymentsController = new PaymentsController(_mockPaymentService.Object, _mockLogger.Object);

            var actionResult = await _paymentsController.CreatePayment(null);
            var result = actionResult as BadRequestResult;

            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }

        [Test]
        public async Task CreatePayment_Failure_ShouldReturn_InternalServerError()
        {
            _mockPaymentService.Setup(p => p.ProcessPayment(It.IsAny<PaymentRequest>())).Throws(new Exception());
            var _paymentsController = new PaymentsController(_mockPaymentService.Object, _mockLogger.Object);

            var actionResult = await _paymentsController.CreatePayment(new PaymentRequest());
            var result = actionResult as ObjectResult;

            result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
        }
    }
}
