using Enterprise.IntegrationPatterns.RabbitMq.Common;
using RabbitMQ.Client;
using System.Text;

namespace Enterprise.IntegrationPatterns.RabbitMq
{
    public class Sender<TMessage> : ChannelEndpoint<TMessage>, ISender<TMessage>
    {
        public Sender(IChannelFactory channelFactory, IMessageConverter messageConverter, string broker, string endpoint) : 
            base(channelFactory, messageConverter, broker, endpoint) { }

        public void Send(TMessage message)
        {
            var body = _messageConverter.Serialize(message);
            _channel.Model.BasicPublish(exchange: _channel.IsExchange ? _channel.EndpointName : "",
                                 routingKey: _channel.IsExchange ? "":_channel.EndpointName,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
