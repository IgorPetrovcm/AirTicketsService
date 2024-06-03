namespace Producer.Core.Application;


public interface IRabbitHost 
{
    void Publish(string data);

    void PublishAsync(string data);
}