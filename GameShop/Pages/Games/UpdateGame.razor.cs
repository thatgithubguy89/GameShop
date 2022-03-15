using GameShop.Models;
using GameShop.Repositories;
using GameShop.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace GameShop.Pages.Games
{
    public partial class UpdateGame : ComponentBase
    {
        [Inject]
        public IGameRepository _gameRepository { get; set; }

        [Inject]
        public IImageService _imageService { get; set; }

        [Inject]
        public NavigationManager _navManager { get; set; }

        [Parameter] public string Id { get; set; }

        private Game game;

        public async Task AddImageToGame(InputFileChangeEventArgs e)
        {
            if (game.ImagePath != null)
            {
                _imageService.DeleteImageFromServer(game.ImagePath);
                game.ImagePath = await _imageService.AddImageToServerAsync(e, game.Title);
            }
            else
            {
                game.ImagePath = await _imageService.AddImageToServerAsync(e, game.Title);
            }
        }

        public void DeleteImageFromGame()
        {
            _imageService.DeleteImageFromServer(game.ImagePath);
            _navManager.NavigateTo("/");
        }

        public async Task EditGame()
        {
            game.LastEditDate = DateTime.Now;
            await _gameRepository.UpdateGameAsync(game);
            _navManager.NavigateTo("/");
        }

        protected override async Task OnInitializedAsync()
        {
            game = await _gameRepository.GetGameByIdAsync(int.Parse(Id));
        }
    }
}
