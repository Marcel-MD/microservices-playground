using CommandService.Models;

namespace CommandService.Data;

public class CommandRepository : ICommandRepository
{
    private readonly AppDbContext _context;

    public CommandRepository(AppDbContext context)
    {
        _context = context;
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() >= 0;
    }

    public IEnumerable<Command> getCommandsForPlatform(int platformId)
    {
        return _context.Commands
            .Where(c => c.PlatformId == platformId);
    }

    public Command getCommandById(int id)
    {
        return _context.Commands.FirstOrDefault(c => c.Id == id);
    }

    public void CreateCommand(Platform platform, Command command)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        command.Platform = platform;
        command.PlatformId = platform.Id;
        _context.Commands.Add(command);
    }

    public void DeleteCommand(Command command)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        _context.Commands.Remove(command);
    }
}