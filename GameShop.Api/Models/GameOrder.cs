using GameShop.Api.Models.Common;

namespace GameShop.Api.Models
{
    public class GameOrder : BaseEntity
    {
        public bool HasBeenReviewed { get; set; }
        public string? OrderNumber { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
