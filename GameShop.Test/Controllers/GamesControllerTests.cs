using AutoMapper;
using GameShop.Api.Controllers;
using GameShop.Api.Interfaces;
using GameShop.Api.Models;
using GameShop.Api.Models.Dtos;
using GameShop.Api.Models.Responses;
using GameShop.Api.Profiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace GameShop.Test.Controllers
{
    public class GamesControllerTests
    {
        Mock<IGameRepository> _mockGameRepository;
        Mock<ILogger<GamesController>> _mockLogger;
        Mock<IPageService<Game, GameDto>> _mockPageService;
        IMapper _mapper;

        private static readonly List<Game> _mockGames = new List<Game>
        {
            new Game { Id = 1, Title = "test" },
            new Game { Id = 2, Title = "test" },
            new Game { Id = 3, Title = "test" }
        };

        private static readonly List<GameDto> _mockGameDtos = new List<GameDto>
        {
            new GameDto { Id = 1, Title = "test" },
            new GameDto { Id = 2, Title = "test" },
            new GameDto { Id = 3, Title = "test" }
        };

        private static readonly PageResponse<GameDto> _mockPageResponse = new PageResponse<GameDto>
        {
            Payload = _mockGameDtos,
            StartingIndex = 1,
            PageTotal = 1
        };

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(c => c.AddProfile(typeof(MappingProfile)));
            _mapper = new Mapper(config);

            _mockGameRepository = new Mock<IGameRepository>();
            _mockLogger = new Mock<ILogger<GamesController>>();
            _mockPageService = new Mock<IPageService<Game, GameDto>>();
        }

        [Test]
        public async Task GetAllGames()
        {
            _mockGameRepository.Setup(g => g.GetAllAsync()).Returns(Task.FromResult(_mockGames));
            _mockPageService.Setup(p => p.Page(It.IsAny<List<Game>>(), It.IsAny<int>(), It.IsAny<float>())).Returns(_mockPageResponse);
            var _gamesController = new GamesController(_mockLogger.Object, _mockGameRepository.Object, _mapper, _mockPageService.Object);

            var actionResult = await _gamesController.GetAllGames(1);
            var result = actionResult as OkObjectResult;

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
            result.Value.ShouldBeOfType<PageResponse<GameDto>>();
        }

        [Test]
        public async Task GetAllGames_Failure_ShouldReturn_InternalServerError()
        {
            _mockGameRepository.Setup(g => g.GetAllAsync()).Throws(new Exception());
            var _gamesController = new GamesController(_mockLogger.Object, _mockGameRepository.Object, _mapper, _mockPageService.Object);

            var actionResult = await _gamesController.GetAllGames(1);
            var result = actionResult as StatusCodeResult;

            result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
        }

        [Test]
        public async Task GetSingleGame()
        {
            _mockGameRepository.Setup(g => g.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new Game()));
            var _gamesController = new GamesController(_mockLogger.Object, _mockGameRepository.Object, _mapper, _mockPageService.Object);

            var actionResult = await _gamesController.GetSingleGame(1);
            var result = actionResult as OkObjectResult;

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
            result.Value.ShouldBeOfType<GameDto>();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public async Task GetSingleGame_GivenInvalidId_ShouldReturn_BadRequest(int id)
        {
            var _gamesController = new GamesController(_mockLogger.Object, _mockGameRepository.Object, _mapper, _mockPageService.Object);

            var actionResult = await _gamesController.GetSingleGame(id);
            var result = actionResult as BadRequestResult;

            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }

        [Test]
        public async Task GetSingleGame_GivenIdForGameThatDoesNotExist_ShouldReturn_NotFound()
        {
            _mockGameRepository.Setup(g => g.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult<Game>(null));
            var _gamesController = new GamesController(_mockLogger.Object, _mockGameRepository.Object, _mapper, _mockPageService.Object);

            var actionResult = await _gamesController.GetSingleGame(1);
            var result = actionResult as NotFoundResult;

            result.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
        }

        [Test]
        public async Task GetSingleGame_Failure_ShouldReturn_InternalServerError()
        {
            _mockGameRepository.Setup(g => g.GetByIdAsync(It.IsAny<int>())).Throws(new Exception());
            var _gamesController = new GamesController(_mockLogger.Object, _mockGameRepository.Object, _mapper, _mockPageService.Object);

            var actionResult = await _gamesController.GetSingleGame(1);
            var result = actionResult as StatusCodeResult;

            result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
        }



        [Test]
        public async Task GetGamesByIds()
        {
            _mockGameRepository.Setup(g => g.GetGamesByIdsAsync(It.IsAny<List<int>>())).Returns(Task.FromResult(new List<Game>()));
            var _gamesController = new GamesController(_mockLogger.Object, _mockGameRepository.Object, _mapper, _mockPageService.Object);

            var actionResult = await _gamesController.GetGamesByIds(new List<int> { 1, 2, 3 });
            var result = actionResult as OkObjectResult;

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
            result.Value.ShouldBeOfType<List<GameDto>>();
        }

        [Test]
        public async Task GetGamesByIds_GivenInvalidListOfIds_ShouldReturn_BadRequest()
        {
            var _gamesController = new GamesController(_mockLogger.Object, _mockGameRepository.Object, _mapper, _mockPageService.Object);

            var actionResult = await _gamesController.GetGamesByIds(new List<int>());
            var result = actionResult as BadRequestResult;

            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }

        [Test]
        public async Task GetGamesByIds_Failure_ShouldReturn_InternalServerError()
        {
            _mockGameRepository.Setup(g => g.GetGamesByIdsAsync(It.IsAny<List<int>>())).Throws(new Exception());
            var _gamesController = new GamesController(_mockLogger.Object, _mockGameRepository.Object, _mapper, _mockPageService.Object);

            var actionResult = await _gamesController.GetGamesByIds(new List<int> { 1 });
            var result = actionResult as StatusCodeResult;

            result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
        }
    }
}
