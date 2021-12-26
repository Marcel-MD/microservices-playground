using CommandService.Models;

namespace CommandService.Data;

public interface ICommandRepository
{
    bool SaveChanges();

    IEnumerable<Command> getCommandsForPlatform(int platformId);
    Command getCommandById(int id);
    void CreateCommand(Platform platform, Command command);
    void DeleteCommand(Command command);
}