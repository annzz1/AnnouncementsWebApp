using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class FileService : IFileService
{
    public async Task<string> SaveFileToDirectory(IFormFile image)
    {
        if (image != null && image.Length > 0)
        {
            var fileName = Path.GetFileName(image.FileName);
            var imagesDirectory = Path.Combine("wwwroot", "images");

            if (!Directory.Exists(imagesDirectory))
            {
                Directory.CreateDirectory(imagesDirectory);
            }

            var filePath = Path.Combine(imagesDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return fileName;
        }

        return string.Empty;
    }
}
