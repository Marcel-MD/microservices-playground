using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            // Source -> Target
            CreateMap<Platform, PlatformDto>();
            CreateMap<CreatePlatformDto, Platform>();
            CreateMap<PlatformDto, EventPlatformDto>();
        }
    }
}