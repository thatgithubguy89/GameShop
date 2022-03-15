using Microsoft.AspNetCore.Components.Forms;

namespace GameShop.Services
{
    public interface IImageService
    {
        Task<string> AddImageToServerAsync(InputFileChangeEventArgs e, string title);
        void DeleteImageFromServer(string path);
    }
}
