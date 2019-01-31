using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace QueuePublisher.MessageService
{
    public class MessageNotify : IMessageNotify<string>
    {
        private ConnectionFactory _connectionFactory;
        private readonly MessageQueueConfig _messageQueueConfig;
        private IModel _model;
        private IConnection _connection;
        private IBasicProperties _properties;

        public MessageNotify(IOptions<MessageQueueConfig> messageQueueConfig)
        {
            _messageQueueConfig = messageQueueConfig.Value;
            SetUpService();
        }
        public void NotifyService(string content)
        {
            byte[] messageBuffer = Encoding.Default.GetBytes(content);
            _model.BasicPublish(_messageQueueConfig.ExchangeName, "", _properties, messageBuffer);
        }

        private void SetUpService()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = _messageQueueConfig.HostName,
                UserName = _messageQueueConfig.UserName,
                Password = _messageQueueConfig.Password
            };
            if (string.IsNullOrEmpty(_messageQueueConfig.VirtualHost) == false)
                _connectionFactory.VirtualHost = _messageQueueConfig.VirtualHost;
            if (_messageQueueConfig.Port > 0)
                _connectionFactory.Port = _messageQueueConfig.Port;
            _connection = _connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
            _properties = _model.CreateBasicProperties();
            _properties.Persistent = true;
        }
    }
}
