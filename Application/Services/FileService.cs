using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

/// <summary>
/// ფაილის სერვისი
/// </summary>
public class FileService : IFileService
{
    /// <summary>
    /// შეინახავს ფაილს wwwroot/images დირექტორიაში
    /// </summary>
    /// <param name="image">ფაილი.</param>
    /// <returns>ფაილის სახელი, თუ წარმატებით შეინახება დოკუმენტი; წინააღმდეგ შემთხვევაში, ცარიელი სტრინგი.</returns>
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
