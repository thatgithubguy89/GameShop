using AutoMapper;
using GameShop.Api.Interfaces;
using GameShop.Api.Models;
using GameShop.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IEmailService emailService, ILogger<OrdersController> logger, IMapper mapper, IOrderRepository orderRepository)
        {
            _emailService = emailService;
            _logger = logger;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        [HttpGet("singleorder/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetSingleOrder(int id)
        {
            try
            {
                _logger.LogInformation("Getting order with the id of {}", id);

                if (id <= 0)
                    return BadRequest();

                var order = await _orderRepository.GetByIdAsync(id);

                return Ok(_mapper.Map<OrderDto>(order));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get order with the id of {}: {}", id, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("createorder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateOrder(OrderDto orderDto)
        {
            try
            {
                _logger.LogInformation("Creating order.");

                if (orderDto == null)
                    return BadRequest();

                var newOrder = await _orderRepository.AddOrderAsync(_mapper.Map<Order>(orderDto), orderDto.GameIds);

                await _emailService.SendEmails(newOrder);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to created order: {}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
