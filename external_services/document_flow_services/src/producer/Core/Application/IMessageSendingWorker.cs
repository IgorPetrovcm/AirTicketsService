namespace Producer.Core.Application;


public interface IMessageSendingWorker 
{
    void SendMessage(object message);

    void SendMessageAsync(object message);
}