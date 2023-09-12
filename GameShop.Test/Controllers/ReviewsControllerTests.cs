using GameShop.Api.Controllers;
using GameShop.Api.Interfaces;
using GameShop.Api.Models;
using GameShop.Api.Models.Dtos;
using GameShop.Api.Models.Requests;
using GameShop.Api.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace GameShop.Test.Controllers
{
    public class ReviewsControllerTests
    {
        Mock<ILogger<ReviewsController>> _mockLogger;
        Mock<IPageService<Review, ReviewDto>> _mockPageService;
        Mock<IReviewRepository> _mockReviewRepository;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<ReviewsController>>();

            _mockPageService = new Mock<IPageService<Review, ReviewDto>>();

            _mockReviewRepository = new Mock<IReviewRepository>();
        }

        [Test]
        public async Task GetAllReviewsForGame()
        {
            _mockReviewRepository.Setup(r => r.GetReviewsByGameIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new List<Review>()));
            _mockPageService.Setup(p => p.Page(It.IsAny<List<Review>>(), It.IsAny<int>(), It.IsAny<float>())).Returns(new PageResponse<ReviewDto>());
            var _reviewsController = new ReviewsController(_mockLogger.Object, _mockPageService.Object, _mockReviewRepository.Object);

            var actionResult = await _reviewsController.GetAllReviewsForGame(new GameReviewsRequest());
            var result = actionResult as OkObjectResult;

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
            result.Value.ShouldBeOfType<PageResponse<ReviewDto>>();
        }

        [Test]
        public async Task GetAllReviewsForGame_GivenInvalidPageRequest_ShouldReturn_BadRequest()
        {
            var _reviewsController = new ReviewsController(_mockLogger.Object, _mockPageService.Object, _mockReviewRepository.Object);

            var actionResult = await _reviewsController.GetAllReviewsForGame(null);
            var result = actionResult as BadRequestResult;

            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }

        [Test]
        public async Task GetAllReviewsForGame_Failure_ShouldReturn_InternalServerError()
        {
            _mockReviewRepository.Setup(r => r.GetReviewsByGameIdAsync(It.IsAny<int>())).Throws(new Exception());
            var _reviewsController = new ReviewsController(_mockLogger.Object, _mockPageService.Object, _mockReviewRepository.Object);

            var actionResult = await _reviewsController.GetAllReviewsForGame(new GameReviewsRequest());
            var result = actionResult as StatusCodeResult;

            result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
        }
    }
}
