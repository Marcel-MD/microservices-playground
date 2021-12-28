using PlatformService.Dtos;

namespace PlatformService.AsyncDataServices;

public interface IMessageBusClient
{
    void PublishNewMessage(EventPlatformDto dto);
}