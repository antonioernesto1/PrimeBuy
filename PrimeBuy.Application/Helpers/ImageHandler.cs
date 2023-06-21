using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace PrimeBuy.Application.Helpers
{
    public class ImageHandler : IImageHandler
    {
        private readonly IWebHostEnvironment _webHostEnviroment;

        public ImageHandler(IWebHostEnvironment webHostEnviroment)
        {
            _webHostEnviroment = webHostEnviroment;
        }

        public string UploadImage(IFormFile image)
        {
            var webRootPath = _webHostEnviroment.WebRootPath;
            string uploadFolder = Path.Combine(webRootPath, "images");

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            string filePath = Path.Combine(uploadFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }

            return $"~/images/{fileName}";
        }
    }
}