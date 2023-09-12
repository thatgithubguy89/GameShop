using GameShop.Api.Models.Common;

namespace GameShop.Api.Models.Dtos
{
    public class ReviewDto : BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int GameId { get; set; }
        public GameDto? Game { get; set; }
    }
}
