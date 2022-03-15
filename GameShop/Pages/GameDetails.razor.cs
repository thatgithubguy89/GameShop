using GameShop.Models;
using GameShop.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

namespace GameShop.Pages
{
    public partial class GameDetails : ComponentBase
    {
        [Inject]
        public IGameRepository _gameRepository { get; set; }

        [Inject]
        public IShoppingCartRepository _shoppingCartRepository { get; set; }

        [Inject]
        public IHttpContextAccessor _http { get; set; }

        [Inject]
        public UserManager<IdentityUser> _userManager { get; set; }

        [Inject]
        public NavigationManager _navManager { get; set; }

        [Parameter] public string Id { get; set; }

        private Game game = new();

        public async Task AddShoppingCartItem()
        {
            var claims = _http.HttpContext.User;
            var user = await _userManager.GetUserAsync(claims);
            await _shoppingCartRepository.UpsertItem(user.UserName, game.Title, game.Price);
        }

        protected override async Task OnInitializedAsync()
        {
            game = await _gameRepository.GetGameByIdAsync(int.Parse(Id));
        }
    }
}
