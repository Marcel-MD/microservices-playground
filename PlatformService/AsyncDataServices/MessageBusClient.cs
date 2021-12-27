using System.Text;
using System.Text.Json;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices;

public class MessageBusClient : IMessageBusClient
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageBusClient(IConfiguration configuration)
    {
        _configuration = configuration;
        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQHost"],
            Port = int.Parse(_configuration["RabbitMQPort"]),
        };

        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            
            Console.WriteLine("--> Connected to Message Bus");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"--> Could not connect to the message buss: {ex.Message}");
        }
    }
    
    public void PublishNewMessage(MessagePlatformDto dto)
    {
        var message = JsonSerializer.Serialize(dto);

        if (!_connection.IsOpen)
        {
            Console.WriteLine("--> RabbitMQ connection is closed, couldn't publish message");
            return;
        }
        
        var body = Encoding.UTF8.GetBytes(message);
        
        _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);

        Console.WriteLine($"--> We have sent {message}");
    }

    public void Dispose()
    {
        Console.WriteLine("--> MessageBus Disposed");

        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }
}