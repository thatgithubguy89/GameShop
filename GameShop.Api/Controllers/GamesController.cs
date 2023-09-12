using AutoMapper;
using GameShop.Api.Interfaces;
using GameShop.Api.Models;
using GameShop.Api.Models.Dtos;
using GameShop.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly ILogger<GamesController> _logger;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly IPageService<Game, GameDto> _pageService;

        public GamesController(ILogger<GamesController> logger, IGameRepository gameRepository, IMapper mapper, IPageService<Game, GameDto> pageService)
        {
            _logger = logger;
            _gameRepository = gameRepository;
            _mapper = mapper;
            _pageService = pageService;
        }

        [HttpGet("allgames/{page}")]
        [ProducesResponseType(typeof(PageResponse<List<Game>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllGames(int page)
        {
            try
            {
                _logger.LogInformation("Getting all games");

                var games = await _gameRepository.GetAllAsync();

                var response = _pageService.Page(games, page);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get all games: {}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("gamesbyids")]
        [ProducesResponseType(typeof(List<GameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetGamesByIds(List<int> ids)
        {
            try
            {
                _logger.LogInformation("Getting cart games");

                if (ids.Count == 0)
                    return BadRequest();

                var games = await _gameRepository.GetGamesByIdsAsync(ids);

                return Ok(_mapper.Map<List<GameDto>>(games));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get cart games: {}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("singlegame/{id}")]
        [ProducesResponseType(typeof(GameDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetSingleGame(int id)
        {
            try
            {
                _logger.LogInformation("Getting game with the id of {}", id);

                if (id <= 0)
                    return BadRequest();

                var game = await _gameRepository.GetByIdAsync(id);
                if (game == null)
                    return NotFound();

                return Ok(_mapper.Map<GameDto>(game));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get game with the id of {}: {}", id, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
