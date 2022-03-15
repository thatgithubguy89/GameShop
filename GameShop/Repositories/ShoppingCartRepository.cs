using GameShop.Data;
using GameShop.Models;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task UpsertItem(string username, string title, decimal price)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(title))
                throw new ArgumentNullException();

            var item = await _context.ShoppingCartItems.FirstOrDefaultAsync(x => x.Username == username && x.Title == title);

            if (item != null)
            {
                item.Amount++;
                item.Price += price;
                item.LastEditDate = DateTime.Now;
            }
            else
            {
                var shoppingCartItem = new ShoppingCartItem
                {
                    Amount = 1,
                    Username = username,
                    Title = title,
                    Price = price,
                    CreateDate = DateTime.Now
                };

                await _context.ShoppingCartItems.AddAsync(shoppingCartItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task ClearCart(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException();

            var shoppingCartItems = await GetItems(username);

            _context.RemoveRange(shoppingCartItems);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItem(string username, string title)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(title))
                throw new ArgumentNullException();

            var shoppingCartItem = await _context.ShoppingCartItems.FirstOrDefaultAsync(i => i.Username == username && i.Title == title);

            if (shoppingCartItem.Amount > 1)
            {
                shoppingCartItem.Amount--;
                shoppingCartItem.Price -= 59.99M;
            }
            else
            {
                _context.ShoppingCartItems.Remove(shoppingCartItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<ShoppingCartItem>> GetItems(string username)
        {
            if(string.IsNullOrEmpty(username))
                throw new ArgumentNullException();

            return await _context.ShoppingCartItems
                .Where(i => i.Username == username)
                .ToListAsync();
        }
    }
}
