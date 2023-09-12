using GameShop.Api.Models.Common;

namespace GameShop.Api.Models
{
    public class Review : BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; }
    }
}
