using GameShop.Models;

namespace GameShop.Repositories
{
    public interface IShoppingCartRepository
    {
        Task UpsertItem(string username, string title, decimal price);
        Task ClearCart(string username);
        Task DeleteItem(string username, string title);
        Task<List<ShoppingCartItem>> GetItems(string username);
    }
}
