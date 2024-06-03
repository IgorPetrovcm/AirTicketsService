namespace Producer.Services;

using Producer.Core.Application;
using RabbitMQ.Client;
using System.Text.Json;

public class MessageSendingWorker : IMessageSendingWorker
{
    private readonly IRabbitHost _rabbitHost;

    private readonly JsonSerializerOptions _serializerOptions;

    public MessageSendingWorker( IRabbitHost rabbitHost)
    {
        _rabbitHost = rabbitHost;

        _serializerOptions = new JsonSerializerOptions(){
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            WriteIndented = true
        };
    }

    public void SendMessage(object message)
    {
        string json = JsonSerializer.Serialize( message, _serializerOptions );

        _rabbitHost.Publish( json );
    }

    public void SendMessageAsync(object message)
    {
        string json = JsonSerializer.Serialize( message, _serializerOptions );

        _rabbitHost.PublishAsync(json);
    }
}