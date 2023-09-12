using GameShop.Api.Models.Common;
using GameShop.Api.Models.Enums;

namespace GameShop.Api.Models
{
    public class Game : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Platform? Platform { get; set; }
        public decimal? Price { get; set; }
        public Publisher? Publisher { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? ImagePath { get; set; }
        public string? TrailerPath { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<GameOrder>? GameOrders { get; set; }
    }
}
