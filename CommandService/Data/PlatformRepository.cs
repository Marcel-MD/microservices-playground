using CommandService.Models;

namespace CommandService.Data;

public class PlatformRepository : IPlatformRepository
{
    private readonly AppDbContext _context;

    public PlatformRepository(AppDbContext context)
    {
        _context = context;
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() >= 0;
    }

    public void CreatePlatform(Platform platform)
    {
        if (platform == null) throw new ArgumentNullException(nameof(platform));

        _context.Platforms.Add(platform);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        return _context.Platforms.ToList();
    }

    public Platform GetPlatformById(int id)
    {
        return _context.Platforms.FirstOrDefault(p => p.Id == id);
    }

    public void DeletePlatform(Platform platform)
    {
        if (platform == null) throw new ArgumentNullException(nameof(platform));

        var commands = _context.Commands.Where(c => c.PlatformId == platform.Id).ToList();

        foreach (var c in commands) _context.Commands.Remove(c);

        _context.Platforms.Remove(platform);
    }

    public bool PlatformExists(int id)
    {
        return _context.Platforms.Any(p => p.Id == id);
    }
}