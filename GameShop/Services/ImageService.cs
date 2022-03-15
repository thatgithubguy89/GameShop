using Microsoft.AspNetCore.Components.Forms;

namespace GameShop.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImageService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public async Task<string> AddImageToServerAsync(InputFileChangeEventArgs e, string title)
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            var files = e.GetMultipleFiles();
            string fileName = title + Path.GetRandomFileName();
            var uploads = Path.Combine(webRootPath, @"images");
            var extension = Path.GetExtension(files[0].Name);

            using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                await files[0].OpenReadStream(5000000).CopyToAsync(fileStream);
            }

            return fileName + extension;
        }

        public void DeleteImageFromServer(string path)
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath + @"\images\" + path);

            if (File.Exists(imagePath))
                File.Delete(imagePath);
        }
    }
}
