using CommandService.Models;

namespace CommandService.Data;

public interface IPlatformRepository
{
    bool SaveChanges();
    IEnumerable<Platform> GetAllPlatforms();
    Platform GetPlatformById(int id);
    void CreatePlatform(Platform platform);
    void DeletePlatform(Platform platform);
    bool PlatformExists(int id);
}