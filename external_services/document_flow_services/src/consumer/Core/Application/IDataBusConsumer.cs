using RabbitMQ.Client.Events;

namespace Consumer.Core.Application;


public interface IDataBusConsumer
{
    void PassMessage( object sender, BasicDeliverEventArgs ea );
}