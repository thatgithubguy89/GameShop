using GameShop.Api.Models.Common;

namespace GameShop.Api.Models.Dtos
{
    public class OrderDto : BaseEntity
    {
        public decimal? Amount { get; set; }
        public int NumberOfGames { get; set; }
        public List<int>? GameIds { get; set; }
        public ICollection<GameOrder>? GameOrders { get; set; }
    }
}
