using GameShop.Models;
using GameShop.Repositories;
using Microsoft.AspNetCore.Components;

namespace GameShop.Pages.Games
{
    public partial class GamesList : ComponentBase
    {
        [Inject]
        public IGameRepository _gameRepository { get; set; }

        private List<Game> games;

        protected override async Task OnInitializedAsync()
        {
            games = await _gameRepository.GetAllGamesAsync();
        }
    }
}
