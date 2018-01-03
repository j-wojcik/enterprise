using Enterprise.IntegrationPatterns.RabbitMq.Common;
using Enterprise.IntegrationPatterns.RabbitMq.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Enterprise.IntegrationPatterns.RabbitMq
{
    public class ChannelFactory : IChannelFactory
    {
        private Dictionary<string,IConnection> _connections = new Dictionary<string, IConnection>();
        private readonly EnterpriseIntegration _configuration;
        private bool _isDisposed = false;

        protected Dictionary<string, IConnection> Connections
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(nameof(Connections));

                return _connections;
            }
        }

        public ChannelFactory(EnterpriseIntegration configuration)
        {
            _configuration = configuration;
        }

        public ChannelFactory()
        {
            _configuration = ConfigurationManager.GetSection("EnterpriseIntegration") as EnterpriseIntegration;
        }

        private IConnection GetConnection(string broker)
        {
            lock (_connections)
            {
                if (Connections.ContainsKey(broker))
                    return Connections[broker];

                var brokerConfiguration = GetBrokerConfiguration(_configuration, broker);

                var connectionFactory = new ConnectionFactory()
                {
                    HostName = brokerConfiguration.Address,
                    VirtualHost = brokerConfiguration.VirtualHost,
                    UserName = brokerConfiguration.Username,
                    Password = brokerConfiguration.Password
                };

                var connection = connectionFactory.CreateConnection();
                Connections[broker] = connection;

                return connection;
            }
        }

        public Channel OpenChannel(string broker, string endpoint)
        {      
            var model = GetConnection(broker).CreateModel();
            //TODO: DRY, refactor durring next iteration
            if (_configuration.Queues[endpoint] != null)
            {
                var config = _configuration.Queues[endpoint];
                model.QueueDeclare(config.Name, config.Durable, config.Exclusive, config.AutoDelete, null);
                return new Channel() { Model = model, IsExchange = false, EndpointName = config.Name };
            }

            if (_configuration.Exchanges[endpoint] != null)
            {
                var config = _configuration.Exchanges[endpoint];
                model.ExchangeDeclare(config.Name, config.ExchangeType.ToString());
                return new Channel() { Model = model, IsExchange = true, EndpointName = config.Name };
            }

            throw new ArgumentException(nameof(broker));
        }

        private Broker GetBrokerConfiguration(EnterpriseIntegration configuration, string broker)
        {
            var brokerConfiguration = configuration.Brokers[broker];

            if (brokerConfiguration == null)
                throw new ArgumentException(nameof(broker));

            return brokerConfiguration;
        }

        public void Dispose()
        {
            lock(_connections)
            {
                foreach (var key in Connections.Keys)
                {
                    Connections[key].Dispose();
                    Connections.Remove(key);
                }
                _isDisposed = true;
            }
        }
    }
}
