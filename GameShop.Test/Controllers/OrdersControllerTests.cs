using AutoMapper;
using GameShop.Api.Controllers;
using GameShop.Api.Interfaces;
using GameShop.Api.Models;
using GameShop.Api.Models.Dtos;
using GameShop.Api.Profiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace GameShop.Test.Controllers
{
    public class OrdersControllerTests
    {
        Mock<IOrderRepository> _mockOrderRepository;
        Mock<IEmailService> _mockEmailService;
        Mock<ILogger<OrdersController>> _mockLogger;
        IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(c => c.AddProfile(typeof(MappingProfile)));
            _mapper = new Mapper(config);

            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockEmailService = new Mock<IEmailService>();
            _mockLogger = new Mock<ILogger<OrdersController>>();
        }

        [Test]
        public async Task GetSingleOrder()
        {
            _mockOrderRepository.Setup(o => o.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new Order()));
            var _ordersController = new OrdersController(_mockEmailService.Object, _mockLogger.Object, _mapper, _mockOrderRepository.Object);

            var actionResult = await _ordersController.GetSingleOrder(1);
            var result = actionResult as OkObjectResult;

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
            result.Value.ShouldBeOfType<OrderDto>();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public async Task GetSingleOrder_GivenInvalidId_ShouldReturn_BadRequest(int id)
        {
            _mockOrderRepository.Setup(o => o.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new Order()));
            var _ordersController = new OrdersController(_mockEmailService.Object, _mockLogger.Object, _mapper, _mockOrderRepository.Object);

            var actionResult = await _ordersController.GetSingleOrder(id);
            var result = actionResult as BadRequestResult;

            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }

        [Test]
        public async Task GetSingleOrder_Failure_ShouldReturn_InternalServerError()
        {
            _mockOrderRepository.Setup(o => o.GetByIdAsync(It.IsAny<int>())).Throws(new Exception());
            var _ordersController = new OrdersController(_mockEmailService.Object, _mockLogger.Object, _mapper, _mockOrderRepository.Object);

            var actionResult = await _ordersController.GetSingleOrder(1);
            var result = actionResult as ObjectResult;

            result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
        }

        [Test]
        public async Task CreateOrder()
        {
            _mockOrderRepository.Setup(o => o.AddOrderAsync(It.IsAny<Order>(), It.IsAny<List<int>>())).Returns(Task.FromResult(new Order()));
            _mockEmailService.Setup(e => e.SendEmails(It.IsAny<Order>()));
            var _ordersController = new OrdersController(_mockEmailService.Object, _mockLogger.Object, _mapper, _mockOrderRepository.Object);

            var actionResult = await _ordersController.CreateOrder(new OrderDto { Id = 1, GameIds = new List<int> { 1, 2 } });
            var result = actionResult as NoContentResult;

            result.StatusCode.ShouldBe(StatusCodes.Status204NoContent);
        }

        [Test]
        public async Task CreateOrder_GivenInvalidOrder_ShouldReturn_BadRequest()
        {
            var _ordersController = new OrdersController(_mockEmailService.Object, _mockLogger.Object, _mapper, _mockOrderRepository.Object);

            var actionResult = await _ordersController.CreateOrder(null);
            var result = actionResult as BadRequestResult;

            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }

        [Test]
        public async Task CreateOrder_Failure_ShouldReturn_InternalServerError()
        {
            _mockOrderRepository.Setup(o => o.AddOrderAsync(It.IsAny<Order>(), It.IsAny<List<int>>())).Throws(new Exception());
            var _ordersController = new OrdersController(_mockEmailService.Object, _mockLogger.Object, _mapper, _mockOrderRepository.Object);

            var actionResult = await _ordersController.CreateOrder(new OrderDto { Id = 1, GameIds = new List<int> { 1, 2 } });
            var result = actionResult as ObjectResult;

            result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
        }
    }
}
