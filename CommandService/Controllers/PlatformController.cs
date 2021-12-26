using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;

[Route("api/c/platforms")]
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

        if (platform == null) return NotFound();

        return Ok(_mapper.Map<PlatformDto>(platform));
    }

    [HttpPost]
    public ActionResult CreatePlatform(CreatePlatformDto dto)
    {
        var platform = _mapper.Map<Platform>(dto);

        _repository.CreatePlatform(platform);
        _repository.SaveChanges();

        Console.WriteLine("--> Inbound POST # Command Service");
        var platformDto = _mapper.Map<PlatformDto>(platform);
        return Ok(platformDto);
    }

    [HttpDelete("{id}")]
    public ActionResult DeletePlatform(int id)
    {
        var platform = _repository.GetPlatformById(id);

        if (platform == null) return NotFound();

        _repository.DeletePlatform(platform);
        _repository.SaveChanges();

        Console.WriteLine("--> Inbound DELETE # Command Service");
        return NoContent();
    }
}