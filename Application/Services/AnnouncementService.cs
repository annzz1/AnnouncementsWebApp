using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.IRepository;
using Domain.Models;

namespace Application.Services;

public class AnnouncementService(IAnnouncementRepository announcementRepository, IFileService fileService, IMapper mapper) : IAnnouncementService
{
    public async Task Add(AnnouncementRequestDto announcement)
    {
        ArgumentNullException.ThrowIfNull(announcement);
        var mappedAnnouncement = mapper.Map<Announcement>(announcement);
        var fileName = await fileService.SaveFileToDirectory(announcement.Image);
        mappedAnnouncement.Image = "/images/" + fileName;
        await announcementRepository.AddAsync(mappedAnnouncement);
    }



    public async Task Delete(Guid id)
    {
        await announcementRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<AnnouncementResponseDto>> GetAll(string? searchTitle)
    {
        var announcements = await announcementRepository.GetAllAsync(searchTitle);
        var announcementDtos = mapper.Map<IEnumerable<AnnouncementResponseDto>>(announcements);
        return announcementDtos is null ? throw new ArgumentNullException(nameof(announcementDtos)) : announcementDtos;
    }

    public async Task<AnnouncementResponseDto> GetById(Guid id)
    {
       var announcement = await announcementRepository.GetByIdAsync(id);
       var announcementDto = mapper.Map<AnnouncementResponseDto>(announcement);
       return announcementDto is null ? throw new ArgumentException("An error occured while fetching the data.") : announcementDto;
    }

    public async Task Update(Guid id, AnnouncementRequestDto announcement)
    {
        ArgumentNullException.ThrowIfNull(announcement);
        var fileName = await fileService.SaveFileToDirectory(announcement.Image);
        var mappedAnnouncement = mapper.Map<Announcement>(announcement);
        mappedAnnouncement.Image = "/images/" + fileName;
        await announcementRepository.UpdateAsync(id, mappedAnnouncement);
    }
}
