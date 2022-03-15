using GameShop.Models;
using GameShop.Repositories;
using GameShop.Services;
using Microsoft.AspNetCore.Components;

namespace GameShop.Pages.Games
{
    public partial class DeleteGame : ComponentBase
    {
        [Inject]
        IGameRepository _gameRepository { get; set; }

        [Inject]
        IImageService _imageService { get; set; }

        [Inject]
        NavigationManager _navManager { get; set; }

        [Parameter] public string Id { get; set; }

        private Game game;

        protected override async Task OnInitializedAsync()
        {
            game = await _gameRepository.GetGameByIdAsync(int.Parse(Id));
        }

        public async Task RemoveGame()
        {
            _imageService.DeleteImageFromServer(game.ImagePath);
            await _gameRepository.DeleteGameAsync(game);
            _navManager.NavigateTo("/");
        }
    }
}
