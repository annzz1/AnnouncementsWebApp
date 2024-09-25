namespace Application.DTOs;

public class AnnouncementDetailsResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public string Phone { get; set; }
}
