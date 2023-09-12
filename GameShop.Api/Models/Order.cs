using GameShop.Api.Models.Common;

namespace GameShop.Api.Models
{
    public class Order : BaseEntity
    {
        public string? OrderNumber { get; set; }
        public decimal? Amount { get; set; }
        public int NumberOfGames { get; set; }
        public ICollection<GameOrder>? GameOrders { get; set; }
    }
}
