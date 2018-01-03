using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.IntegrationPatterns.RabbitMq.Common
{
    public abstract class ChannelEndpoint<TMessage> : IDisposable
    { 
        private readonly IChannelFactory _channelFactory;
        protected readonly Channel _channel;
        protected readonly IMessageConverter _messageConverter;
        protected readonly string _endpoint;

        public ChannelEndpoint(IChannelFactory channelFactory, IMessageConverter messageConverter, string broker, string endpoint)
        {
            _channelFactory = channelFactory ?? throw new ArgumentException(nameof(channelFactory));
            _channel = _channelFactory.OpenChannel(broker, endpoint);
            _messageConverter = messageConverter ?? throw new ArgumentException(nameof(channelFactory));
            _endpoint = endpoint;
        }

        public virtual void Dispose()
        {
            _channel.Model.Dispose();
        }
    }
}
