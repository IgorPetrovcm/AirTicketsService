namespace Consumer.Core.Application;

using Consumer.Core.Model;

public interface IMailHost
{
    void SendMessage(string message, string destination);

    Task SendMessageAsync(string message, string destination);

}