using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;

namespace CommandService.Profiles;

public class PlatformProfile : Profile
{
    public PlatformProfile()
    {
        CreateMap<Platform, PlatformDto>();
        CreateMap<CreatePlatformDto, Platform>();
        CreateMap<EventPlatformDto, Platform>();
    }
}