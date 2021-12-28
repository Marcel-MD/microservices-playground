using System.Text;
using CommandService.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CommandService.AsyncDataServices;

public class MessageBusSubscriber : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IEventProcessor _eventProcessor;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;

    public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
    {
        _configuration = configuration;
        _eventProcessor = eventProcessor;

        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQHost"],
            Port = int.Parse(_configuration["RabbitMQPort"])
        };

        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare("trigger", ExchangeType.Fanout);
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(_queueName, "trigger", "");

            Console.WriteLine("--> Listening to Message Bus");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not connect to the message buss: {ex.Message}");
        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (ModuleHandle, ea) =>
        {
            Console.WriteLine("--> Event Received");

            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            _eventProcessor.ProcessEvent(message);
        };

        try
        {
            _channel.BasicConsume(_queueName, true, consumer);
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Could not consume: {e}");
        }

        return Task.CompletedTask;
    }
}