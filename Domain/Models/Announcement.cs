using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Announcement
{
    [Key]
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string Image { get; set; }

    [Required]
    public string Phone { get; set; }
}
