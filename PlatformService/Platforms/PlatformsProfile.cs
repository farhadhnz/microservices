using AutoMapper;
using PlatformService.DTOs;
using PlatformService.Models;

namespace PlatformService.Platforms
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            //   Source ===> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
        }
    }
}