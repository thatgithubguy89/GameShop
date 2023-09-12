using AutoMapper;
using GameShop.Api.Controllers;
using GameShop.Api.Interfaces;
using GameShop.Api.Models.Dtos;
using GameShop.Api.Models;
using Microsoft.Extensions.Logging;
using Moq;
using GameShop.Api.Profiles;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Microsoft.AspNetCore.Http;

namespace GameShop.Test.Controllers
{
    public class GameOrdersControllerTests
    {
        Mock<IGameOrderRepository> _mockGameOrderRepository;
        Mock<ILogger<GameOrdersController>> _mockLogger;
        IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(c => c.AddProfile(typeof(MappingProfile)));
            _mapper = new Mapper(config);

            _mockGameOrderRepository = new Mock<IGameOrderRepository>();
            _mockLogger = new Mock<ILogger<GameOrdersController>>();
        }

        [Test]
        public async Task GetSingleGameOrder()
        {
            _mockGameOrderRepository.Setup(go => go.GetByGameIdAndOrderNumber(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(new GameOrder()));
            var _gameOrdersController = new GameOrdersController(_mockGameOrderRepository.Object, _mockLogger.Object, _mapper);

            var actionResult = await _gameOrdersController.GetSingleGameOrder(1, "123");
            var result = actionResult as OkObjectResult;

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
            result.Value.ShouldBeOfType<GameOrderDto>();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public async Task GetSingleGameOrder_GivenInvalidGameId_ShouldReturn_BadRequest(int gameId)
        {
            var _gameOrdersController = new GameOrdersController(_mockGameOrderRepository.Object, _mockLogger.Object, _mapper);

            var actionResult = await _gameOrdersController.GetSingleGameOrder(gameId, "123");
            var result = actionResult as BadRequestResult;

            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task GetSingleGameOrder_GivenInvalidOrderNumber_ShouldReturn_BadRequest(string orderNumber)
        {
            var _gameOrdersController = new GameOrdersController(_mockGameOrderRepository.Object, _mockLogger.Object, _mapper);

            var actionResult = await _gameOrdersController.GetSingleGameOrder(1, orderNumber);
            var result = actionResult as BadRequestResult;

            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }

        [Test]
        public async Task GetSingleGameOrder_Failure_ShouldReturn_InternalServerError()
        {
            _mockGameOrderRepository.Setup(go => go.GetByGameIdAndOrderNumber(It.IsAny<int>(), It.IsAny<string>())).Throws(new Exception());
            var _gameOrdersController = new GameOrdersController(_mockGameOrderRepository.Object, _mockLogger.Object, _mapper);

            var actionResult = await _gameOrdersController.GetSingleGameOrder(1, "123");
            var result = actionResult as ObjectResult;

            result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
        }

        [Test]
        public async Task UpdateGameOrder()
        {
            _mockGameOrderRepository.Setup(go => go.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new GameOrder()));
            _mockGameOrderRepository.Setup(go => go.UpdateGameOrderAsync(It.IsAny<GameOrder>()));
            var _gameOrdersController = new GameOrdersController(_mockGameOrderRepository.Object, _mockLogger.Object, _mapper);

            var actionResult = await _gameOrdersController.UpdateGameOrder(1);
            var result = actionResult as NoContentResult;

            result.StatusCode.ShouldBe(StatusCodes.Status204NoContent);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public async Task UpdateGameOrder_GivenInvalidId_ShouldReturn_BadRequest(int id)
        {
            var _gameOrdersController = new GameOrdersController(_mockGameOrderRepository.Object, _mockLogger.Object, _mapper);

            var actionResult = await _gameOrdersController.UpdateGameOrder(id);
            var result = actionResult as BadRequestResult;

            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }

        [Test]
        public async Task UpdateGameOrder_GivenIdForGameOrderThatDoesNotExist_ShouldReturn_NotFound()
        {
            var _gameOrdersController = new GameOrdersController(_mockGameOrderRepository.Object, _mockLogger.Object, _mapper);

            var actionResult = await _gameOrdersController.UpdateGameOrder(1);
            var result = actionResult as NotFoundResult;

            result.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
        }

        [Test]
        public async Task UpdateGameOrder_Failure_ShouldReturn_InternalServerError()
        {
            _mockGameOrderRepository.Setup(go => go.GetByIdAsync(It.IsAny<int>())).Throws(new Exception());
            var _gameOrdersController = new GameOrdersController(_mockGameOrderRepository.Object, _mockLogger.Object, _mapper);

            var actionResult = await _gameOrdersController.UpdateGameOrder(1);
            var result = actionResult as ObjectResult;

            result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
        }
    }
}
