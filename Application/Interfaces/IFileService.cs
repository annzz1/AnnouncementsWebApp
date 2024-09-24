using Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IFileService
{
    Task<string> SaveFileToDirectory(IFormFile image);
}
