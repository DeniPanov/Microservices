using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo repo;
        private readonly IMapper mapper;

        public PlatformsController(IPlatformRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<PlatformReadDto> GetPlatforms()
        {
            Console.WriteLine("--> Getting platforms...");

            var platforms = this.repo.GetAllPlatforms();

            return Ok(this.mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platform = this.repo.GetPlatformById(id);

            if (platform == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<PlatformReadDto>(platform));
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto model)
        {
            var platform = this.mapper.Map<Platform>(model);
            
            this.repo.CreatePlatform(platform);
            this.repo.SaveChanges();

            var platformReadDto = this.mapper.Map<PlatformReadDto>(platform);
            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
        }
    }
}
