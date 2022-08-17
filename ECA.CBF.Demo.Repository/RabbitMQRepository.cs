using ECA.CBF.Demo.Repository.Interface;
using ECA.CBF.Demo.Util;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Text;

namespace ECA.CBF.Demo.Repository
{
    public class RabbitMQRepository : IRabbitMQRepository
    {
        private readonly ILogger<RabbitMQRepository> _logger;
        private readonly Uri _rabbitMqUri;

        public RabbitMQRepository(ILogger<RabbitMQRepository> logger)
        {
            _logger = logger;
            _rabbitMqUri = new Uri(EnvConfiguration.Get().RabbitMQConfig.Uri);
        }

        public void SendMessageToQueue(string queueName, string messageText)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    Uri = _rabbitMqUri
                };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                
                channel.QueueDeclare(queue: queueName,
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

                var message = messageText;
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
                _logger.LogInformation(" [x] Sent {0}", message);
            }
            catch (Exception ex)
            {
                string message = $"Exception: {ex.GetType().FullName} Message: {ex.Message}";
                _logger.LogError(message);
            }
        }
    }
}