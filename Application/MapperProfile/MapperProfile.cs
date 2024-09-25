using Application.DTOs;
using AutoMapper;
using Domain.Models;
using System.Xml.Serialization;

namespace Application.MapperProfile;

/// <summary>
/// ვიყენებთ data transfer object (DTO) და უშუალოდ მოდელის მეპინგისთვის.
/// </summary>
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<AnnouncementRequestDto, Announcement>();
        CreateMap<Announcement, AnnouncementResponseDto>();
        CreateMap<Announcement, AnnouncementDetailsResponseDto>();
    }
}
