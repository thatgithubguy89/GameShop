using GameShop.Api.Interfaces;
using GameShop.Api.Models;
using GameShop.Api.Models.Dtos;
using GameShop.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ILogger<ReviewsController> _logger;
        private readonly IPageService<Review, ReviewDto> _pageService;
        private readonly IReviewRepository _reviewRepository;

        public ReviewsController(ILogger<ReviewsController> logger, IPageService<Review, ReviewDto> pageService, IReviewRepository reviewRepository)
        {
            _logger = logger;
            _pageService = pageService;
            _reviewRepository = reviewRepository;
        }

        [HttpPost("createreview")]
        [ProducesResponseType(typeof(GameReviewsRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateReview(Review review)
        {
            try
            {
                _logger.LogInformation("Creating Review");

                if (review == null)
                    return BadRequest();

                await _reviewRepository.AddAsync(review);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create review: {}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("gamereviews")]
        [ProducesResponseType(typeof(GameReviewsRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllReviewsForGame([FromBody] GameReviewsRequest request)
        {
            try
            {
                _logger.LogInformation("Getting reviews for game with the id of {}", request?.GameId);

                if (request == null)
                    return BadRequest();

                var reviews = await _reviewRepository.GetReviewsByGameIdAsync(request.GameId);

                var response = _pageService.Page(reviews, request.Page, 3);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get reviews for game with the id of {}: {}", request.GameId, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
