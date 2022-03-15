using GameShop.Models.Common;

namespace GameShop.Models
{
    public class ShoppingCartItem : BaseEntity
    {
        public int Amount { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
