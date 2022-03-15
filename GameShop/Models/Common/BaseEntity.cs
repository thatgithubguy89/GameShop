namespace GameShop.Models.Common
{
    public class BaseEntity
    { 
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastEditDate { get; set; }
    }
}
