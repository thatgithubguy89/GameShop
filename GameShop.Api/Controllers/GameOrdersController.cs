using AutoMapper;
using GameShop.Api.Interfaces;
using GameShop.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameOrdersController : ControllerBase
    {
        private readonly IGameOrderRepository _gameOrderRepository;
        private readonly ILogger<GameOrdersController> _logger;
        private readonly IMapper _mapper;

        public GameOrdersController(IGameOrderRepository gameOrderRepository, ILogger<GameOrdersController> logger, IMapper mapper)
        {
            _gameOrderRepository = gameOrderRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("singlegameorder/{gameId}/{orderNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetSingleGameOrder(int gameId, string orderNumber)
        {
            try
            {
                _logger.LogInformation("Getting game order");

                if (gameId <= 0)
                    return BadRequest();
                if (string.IsNullOrWhiteSpace(orderNumber))
                    return BadRequest();

                var gameOrder = await _gameOrderRepository.GetByGameIdAndOrderNumber(gameId, orderNumber);

                return Ok(_mapper.Map<GameOrderDto>(gameOrder));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get game order: {}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("updategameorder/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateGameOrder(int id)
        {
            try
            {
                _logger.LogInformation("Getting game order");

                if (id <= 0)
                    return BadRequest();

                var gameOrder = await _gameOrderRepository.GetByIdAsync(id);
                if (gameOrder == null)
                    return NotFound();

                await _gameOrderRepository.UpdateGameOrderAsync(gameOrder);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get game order: {}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
