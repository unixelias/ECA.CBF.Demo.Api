namespace ECA.CBF.Demo.Repository.Interface
{
    public interface IRabbitMQRepository
    {
        void SendMessageToQueue(string queueName, string messageText);
    }
}