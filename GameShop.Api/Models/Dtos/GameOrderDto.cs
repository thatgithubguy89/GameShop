using GameShop.Api.Models.Common;

namespace GameShop.Api.Models.Dtos
{
    public class GameOrderDto : BaseEntity
    {
        public bool HasBeenReviewed { get; set; }
        public string? OrderNumber { get; set; }
        public int GameId { get; set; }
        public GameDto? Game { get; set; }
        public int OrderId { get; set; }
        public OrderDto? Order { get; set; }
    }
}
