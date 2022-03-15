using GameShop.Models.Common;

namespace GameShop.Models
{
    public class Game : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string TrailerLink { get; set; }
        public string ImagePath { get; set; }
        public List<Review>? Reviews { get; set; }
    }
}
