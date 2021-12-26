using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;

[Route("api/c/platforms/{platformId}/commands")]
[ApiController]
public class CommandController : ControllerBase
{
    private readonly ICommandRepository _commandRepository;
    private readonly IMapper _mapper;
    private readonly IPlatformRepository _platformRepository;

    public CommandController(ICommandRepository commandRepository, IPlatformRepository platformRepository,
        IMapper mapper)
    {
        _commandRepository = commandRepository;
        _platformRepository = platformRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandDto>> GetCommandsForPlatform(int platformId)
    {
        if (!_platformRepository.PlatformExists(platformId)) return NotFound();

        var commands = _commandRepository.getCommandsForPlatform(platformId);
        return Ok(_mapper.Map<IEnumerable<CommandDto>>(commands));
    }

    [HttpGet("{id}")]
    public ActionResult<CommandDto> GetCommandById(int id)
    {
        var command = _commandRepository.getCommandById(id);

        if (command == null) return NotFound();

        return Ok(_mapper.Map<CommandDto>(command));
    }

    [HttpPost]
    public ActionResult<CommandDto> CreateCommand(int platformId, CreateCommandDto dto)
    {
        var command = _mapper.Map<Command>(dto);
        var platform = _platformRepository.GetPlatformById(platformId);

        if (platform == null) return NotFound();

        _commandRepository.CreateCommand(platform, command);
        _commandRepository.SaveChanges();

        var commandDto = _mapper.Map<CommandDto>(command);
        return Ok(commandDto);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCommandById(int id)
    {
        var command = _commandRepository.getCommandById(id);

        if (command == null) return NotFound();

        _commandRepository.DeleteCommand(command);
        _commandRepository.SaveChanges();

        return NoContent();
    }
}