using GameShop.Models;
using GameShop.Repositories;
using GameShop.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace GameShop.Pages.Games
{
    public partial class CreateGame
    {
        [Inject]
        public IGameRepository _gameRepository { get; set; }

        [Inject]
        public IImageService _imageService { get; set; }

        [Inject]
        public NavigationManager _navManager { get; set; }
        
        public Game game = new();

        public async Task AddGame()
        {
            await _gameRepository.AddGameAsync(game);
            _navManager.NavigateTo("/");
        }

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
            _navManager.NavigateTo("/gameslist");
        }
    }
}
