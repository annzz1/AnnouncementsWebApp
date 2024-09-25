using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.IRepository;
using Domain.Models;

namespace Application.Services;

/// <summary>
/// განაცხადებების სერვისი, dependency injection - ით ვამატებთ შესაბამის სერვისებს, მეპერს და რეპოზიტორიას.
/// </summary>
public class AnnouncementService(IAnnouncementRepository announcementRepository, IFileService fileService, IMapper mapper) : IAnnouncementService
{
    /// <summary>
    /// ასინქრონული დამატება ახალი განცხადების.
    /// </summary>
    /// <param name="announcement">გადაეცემა განცხადების შესაძებ ინფორმაცია AnnouncementRequestDto საშუალებით.</param>
    public async Task Add(AnnouncementRequestDto announcement)
    {
        ArgumentNullException.ThrowIfNull(announcement);
        var mappedAnnouncement = mapper.Map<Announcement>(announcement);
        var fileName = await fileService.SaveFileToDirectory(announcement.Image);
        mappedAnnouncement.Image = "/images/" + fileName;
        await announcementRepository.AddAsync(mappedAnnouncement);
    }

    /// <summary>
    /// ასინქრონული წაშლა განცხადების, მისი უნიკალური იდენტიფიკატორით.
    /// </summary>
    /// <param name="id">Guid ტიპის უნიკალური იდენტიფიკატორი.</param>
    public async Task Delete(Guid id)
    {
        await announcementRepository.DeleteAsync(id);
    }

    /// <summary>
    /// ასინქრონულად ყველა განცხადების დაბრუნება
    /// </summary>
    /// <param name="searchTitle">სათაური, რომლითაც ხდება განცხადებების ძიება (არასავალდებულო).</param>
    /// <returns>განცხადებების სია DTO ფორმატში.</returns>
    public async Task<IEnumerable<AnnouncementResponseDto>> GetAll(string? searchTitle)
    {
        var announcements = await announcementRepository.GetAllAsync(searchTitle);
        var announcementDtos = mapper.Map<IEnumerable<AnnouncementResponseDto>>(announcements);
        return announcementDtos is null ? throw new ArgumentNullException(nameof(announcementDtos)) : announcementDtos;
    }

    /// <summary>
    /// ასინქრონული დაბრუნება განცხადების მისი უნიკალური იდენტიფიკატორით
    /// </summary>
    /// <param name="id">Guid ტიპის უნიკალური იდენტიფიკატორი.</param>
    /// <returns>განცხადების დეტალები DTO ფორმატში.</returns>
    public async Task<AnnouncementDetailsResponseDto> GetById(Guid id)
    {
        var announcement = await announcementRepository.GetByIdAsync(id);
        var announcementDto = mapper.Map<AnnouncementDetailsResponseDto>(announcement);
        return announcementDto is null ? throw new ArgumentException("An error occured while fetching the data.") : announcementDto;
    }

    /// <summary>
    /// ასინქრონული განახლება არსებული განცხადების.
    /// </summary>
    /// <param name="id">Guid ტიპის განცხადების უნიკალური იდენტიფიკატორი.</param>
    /// <param name="announcement">განახლებული განცხადების დეტალები.</param>
    public async Task Update(Guid id, AnnouncementRequestDto announcement)
    {
        ArgumentNullException.ThrowIfNull(announcement);
        var fileName = await fileService.SaveFileToDirectory(announcement.Image);
        var mappedAnnouncement = mapper.Map<Announcement>(announcement);
        mappedAnnouncement.Image = "/images/" + fileName;
        await announcementRepository.UpdateAsync(id, mappedAnnouncement);
    }
}
