using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
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
        private readonly IMessageBusClient _client;

        public PlatformController(IPlatformRepository repository, IMapper mapper, IMessageBusClient client)
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
                var messageDto = _mapper.Map<EventPlatformDto>(platformDto);
                messageDto.Event = "CreatePlatform";
                _client.PublishNewMessage(messageDto);
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Could not send async message: {e.Message}");
            }

            return Ok(platformDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlatform(int id)
        {
            var platform = _repository.GetPlatformById(id);

            if (platform == null) {
                return NotFound();
            }

            try
            {
                var platformDto = _mapper.Map<PlatformDto>(platform);
                var messageDto = _mapper.Map<EventPlatformDto>(platformDto);
                messageDto.Event = "DeletePlatform";
                _client.PublishNewMessage(messageDto);
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Could not send async message: {e.Message}");
            }

            _repository.DeletePlatform(platform);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}