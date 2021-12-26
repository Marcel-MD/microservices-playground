using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http 
{
    public interface ICommandDataClient 
    {
        Task SendPlatformToCommand(PlatformDto dto);

        Task RemovePlatformFromCommand(int id);
    }
}