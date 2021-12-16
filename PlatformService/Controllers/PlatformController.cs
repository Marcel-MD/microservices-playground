using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/platforms")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;

        public PlatformController(IPlatformRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformDto>> GetPlatforms()
        {
            var platforms = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformDto>>(platforms));
        }

        [HttpGet("{id}")]
        public ActionResult<PlatformDto> GetPlatformById(int id)
        {
            var platform = _repository.GetPlatformById(id);
            return Ok(_mapper.Map<PlatformDto>(platform));
        }

        [HttpPost]
        public ActionResult CreatePlatform(CreatePlatformDto dto)
        {
            _repository.CreatePlatform(_mapper.Map<Platform>(dto));
            _repository.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePlatform(int id)
        {
            var platform = _repository.GetPlatformById(id);
            _repository.DeletePlatform(platform);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}