using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Application.MapperProfile;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<AnnouncementRequestDto, Announcement>();
        CreateMap<Announcement, AnnouncementResponseDto>();
    }
}
