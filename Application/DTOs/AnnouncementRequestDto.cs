using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class AnnouncementRequestDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Title must be at least 3 characters long.")]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    public IFormFile Image { get; set; }

    [Required]
    [Phone]
    public string Phone { get; set; }
}
