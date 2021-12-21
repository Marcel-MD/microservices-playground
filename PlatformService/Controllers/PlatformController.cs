using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [Route("api/platforms")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _client;

        public PlatformController(IPlatformRepository repository, IMapper mapper, ICommandDataClient client)
        {
            _repository = repository;
            _mapper = mapper;
            _client = client;
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

            if (platform == null) {
                return NotFound();
            }

            return Ok(_mapper.Map<PlatformDto>(platform));
        }

        [HttpPost]
        public async Task<ActionResult<PlatformDto>> CreatePlatform(CreatePlatformDto dto)
        {
            var platform = _mapper.Map<Platform>(dto);
            _repository.CreatePlatform(platform);
            _repository.SaveChanges();
            
            var platformDto = _mapper.Map<PlatformDto>(platform);
            
            try
            {
                await _client.SendPlatformToCommand(platformDto);
            }
            catch(Exception exception)
            {
                Console.WriteLine("--> Could not send sync data");
            }

            return Ok(platformDto);
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePlatform(int id)
        {
            var platform = _repository.GetPlatformById(id);

            if (platform == null) {
                return NotFound();
            }
            
            _repository.DeletePlatform(platform);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}