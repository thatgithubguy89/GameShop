using GameShop.Models;
using GameShop.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

namespace GameShop.Pages
{
    public partial class ShoppingCart
    {
        [Inject]
        public IHttpContextAccessor _http { get; set; }

        [Inject]
        public UserManager<IdentityUser> _userManager { get; set; }

        [Inject]
        public NavigationManager _navManager { get; set; }

        [Inject]
        public IShoppingCartRepository _shoppingCartRepository { get; set; }

        private string username = "";
        private List<ShoppingCartItem> shoppingCartItems;
        private Decimal total;

        public async Task ClearCart()
        {
            await _shoppingCartRepository.ClearCart(username);
            total = 0;
            _navManager.NavigateTo("/shoppingcart", true);
        }

        public async Task DeleteItem(string title)
        {
            await _shoppingCartRepository.DeleteItem(username, title);
            _navManager.NavigateTo("/shoppingcart", true);
        }

        protected override async Task OnInitializedAsync()
        {
            var claims = _http.HttpContext.User;
            var user = await _userManager.GetUserAsync(claims);
            username = user.UserName;
            shoppingCartItems = await _shoppingCartRepository.GetItems(username);
            total = shoppingCartItems.Sum(i => i.Price);
        }
    }
}
