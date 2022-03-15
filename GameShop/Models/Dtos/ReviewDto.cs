using GameShop.Models.Common;

namespace GameShop.Models.Dtos
{
    public class ReviewDto : BaseEntity
    {
        public string Content { get; set; }
        public string Username { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
