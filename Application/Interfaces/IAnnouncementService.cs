using Application.DTOs;
namespace Application.Interfaces;

public interface IAnnouncementService
{
    Task Add(AnnouncementRequestDto announcement);

    Task<IEnumerable<AnnouncementResponseDto>> GetAll(string? searchTitle);

    Task<AnnouncementDetailsResponseDto> GetById(Guid id);

    Task Update(Guid id, AnnouncementRequestDto announcement);

    Task Delete(Guid id);
}
