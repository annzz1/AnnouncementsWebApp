namespace Application.DTOs;

public class AnnouncementResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }

    public string Image { get; set; }

    public string? Description { get; set; }

    public string? Phone { get; set; }
}
