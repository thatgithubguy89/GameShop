using GameShop.Models;
using GameShop.Repositories;
using Microsoft.AspNetCore.Components;

namespace GameShop.Pages
{
    public partial class Index
    {
        [Inject]
        public IGameRepository _gameRepository { get; set; }

        [Inject]
        public NavigationManager _navManager { get; set; }
        
        private List<Game> games;

        protected override async Task OnInitializedAsync()
        {
            games = await _gameRepository.GetAllGamesAsync();
        }
    }
}
