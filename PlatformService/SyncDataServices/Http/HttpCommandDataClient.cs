using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendPlatformToCommand(PlatformDto dto)
        {
            var uri = _configuration["CommandServiceUri"];
            var response = await _httpClient.PostAsJsonAsync<PlatformDto>(uri, dto);
            
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Post to command service was successful!");
            }
        }

        public async Task RemovePlatformFromCommand(int id)
        {
            var uri = _configuration["CommandServiceUri"] + id;
            var response = await _httpClient.DeleteAsync(uri);

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Delete to command service was successful!");
            }
        }
    }
}