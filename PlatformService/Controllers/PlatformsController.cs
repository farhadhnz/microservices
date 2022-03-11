using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo repository;
        private readonly IMapper mapper;

        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms......");

            var platformItems = repository.GetAllPlatforms();

            return Ok(mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }


        [HttpGet("{id}", Name = nameof(GetPlatformById))]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platformItem = repository.GetPlatformById(id);

            if (platformItem != null)
            {
                return Ok(mapper.Map<PlatformReadDto>(platformItem));
            }

            return NotFound();
        }


        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platform = mapper.Map<Platform>(platformCreateDto);

            repository.CreatePlatform(platform);
            repository.SaveChanges();

            var PlatformReadDto = mapper.Map<PlatformReadDto>(platform);

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = PlatformReadDto.Id }, PlatformReadDto);
        }
    }
}