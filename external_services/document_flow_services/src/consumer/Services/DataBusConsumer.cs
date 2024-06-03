namespace Consumer.Services;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Consumer.Core.Application;
using DocumentFlowService.GeneralModel;
using DocumentFlowService.GeneralModel.DTOs;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Localization;

public class DataBusConsumer : BackgroundService
{
    private readonly IMailHost _mailHost;

    private readonly IModel _channel;

    private readonly IConnection _connection;

    private readonly JsonSerializerOptions _jsonOptions;

    public DataBusConsumer( IMailHost mailHost, IConfiguration config )
    {
        _mailHost = mailHost;

        ConnectionFactory factory = new ConnectionFactory(){
            HostName = "localhost",
            Port = int.Parse( Environment.GetEnvironmentVariable("DATABUS_PORT") ),
            UserName = Environment.GetEnvironmentVariable("DATABUS_USERNAME"),
            Password = Environment.GetEnvironmentVariable("DATABUS_PASSWORD")
        }; 

        _connection = factory.CreateConnection();

        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: "mail",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        _jsonOptions = new JsonSerializerOptions(){
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            WriteIndented = true
        };
    }

    public void PassMessage( object sender, BasicDeliverEventArgs ea )
    {
        string message = Encoding.UTF8.GetString( ea.Body.ToArray() );

        TicketRequestDTO ticketDTO;

        try {
            ticketDTO = JsonSerializer.Deserialize< TicketRequestDTO >(message, _jsonOptions);
        }
        catch (Exception ex){
            Console.WriteLine("Source: " + ex.Source + "\nMessage: " + ex.Message);
            return;
        }

        string ticket = JsonSerializer.Serialize( ticketDTO.Ticket );

        _mailHost.SendMessageAsync( ticket, ticketDTO.Destination );

        _channel.BasicAck( ea.DeliveryTag, false );
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        EventingBasicConsumer consumer = new EventingBasicConsumer(_channel);

        consumer.Received += PassMessage;

        _channel.BasicConsume( "mail", false, consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel.Dispose();
        _connection.Close();
        base.Dispose();
    }
}