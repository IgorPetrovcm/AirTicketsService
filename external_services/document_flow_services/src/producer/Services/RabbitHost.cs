namespace Producer.Services;

using System.Threading.Tasks;
using RabbitMQ.Client;
using Producer.Core.Application;
using System.Text;

public class RabbitHost : IRabbitHost
{

    private readonly IModel _channel; 

    public RabbitHost(IConfiguration config)
    {
        ConnectionFactory factory = new ConnectionFactory(){
            HostName = "localhost",
            Port = config.GetValue<int>("DATABUS_PORT"),
            UserName = config.GetValue<string>("DATABUS_USERNAME"),
            Password = config.GetValue<string>("DATABUS_PASSWORD")
        }; 

        IConnection connection = factory.CreateConnection();

        _channel = connection.CreateModel();

        _channel.QueueDeclare(
            queue: "mail",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
    }
    
    public void Publish(string data)
    {
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);

        try {
            _channel.BasicPublish(
                exchange: string.Empty,
                routingKey: "mail",
                basicProperties: null,
                body: dataBytes
            );
        }
        catch (Exception ex){
            Console.WriteLine(ex.Message);
        }
    }

    public void PublishAsync(string data)
    {
        Task.Run ( () => Publish( data ));
    }
}