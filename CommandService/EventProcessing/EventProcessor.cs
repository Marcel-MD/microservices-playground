using System.Text.Json;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;

namespace CommandService.EventProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;

    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
        _scopeFactory = scopeFactory;
        _mapper = mapper;
    }

    public void ProcessEvent(string message)
    {
        var eventType = JsonSerializer.Deserialize<GenericEventDto>(message);

        if (!eventType.Event.Contains("Platform")) return;

        var platformDto = JsonSerializer.Deserialize<EventPlatformDto>(message);
        var platform = _mapper.Map<Platform>(platformDto);

        switch (platformDto.Event)
        {
            case "CreatePlatform":
                AddPlatform(platform);
                break;
            case "DeletePlatform":
                DeletePlatform(platform);
                break;
        }
    }

    private void AddPlatform(Platform platform)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IPlatformRepository>();

            if (repo.PlatformExists(platform.Id)) return;

            repo.CreatePlatform(platform);
            repo.SaveChanges();
            Console.WriteLine($"--> Created Platform: {platform.Name}");
        }
    }

    private void DeletePlatform(Platform platform)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IPlatformRepository>();

            if (!repo.PlatformExists(platform.Id)) return;

            repo.DeletePlatform(platform);
            repo.SaveChanges();
            Console.WriteLine($"--> Deleted Platform: {platform.Name}");
        }
    }
}