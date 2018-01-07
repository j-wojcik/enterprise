using Enterprise.IntegrationPatterns.RabbitMq.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace Enterprise.IntegrationPatterns.RabbitMq
{
    public class RPCServer<TArgument, TReturn> : ChannelEndpoint<TArgument>
    {
        public delegate TReturn MethodToCall(TArgument argument);
        public MethodToCall _methodDelegate;
        private EventingBasicConsumer _consumer;

        public RPCServer(IChannelFactory channelFactory, IMessageConverter messageConverter, string broker, string endpoint) : 
            base(channelFactory, messageConverter, broker, endpoint)
        {
        }

        public void Start(MethodToCall methodDelegate)
        {
            _methodDelegate = methodDelegate;

            _consumer = new EventingBasicConsumer(_channel.Model);
            _consumer.Received += _consumer_Received;
            _channel.Model.BasicConsume(queue: _channel.EndpointName,
                                 autoAck: true,
                                 consumer: _consumer);
        }

        private void _consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            byte[] response = null;
            var message = _messageConverter.Deserialize<TArgument>(e.Body);
            var replyProperties = _channel.Model.CreateBasicProperties();
            replyProperties.CorrelationId = e.BasicProperties.CorrelationId;

            try
            {
                TReturn result = _methodDelegate(message);
                response = _messageConverter.Serialize(result);
            }
            catch(Exception ex)
            {
                response = _messageConverter.Serialize(new Exception(ex.Message));
            }
            finally
            {
                _channel.Model.BasicPublish("", e.BasicProperties.ReplyTo, replyProperties, response);
                _channel.Model.BasicAck(e.DeliveryTag, false);
            }
        }

        public override void Dispose()
        {
            _consumer.Received -= _consumer_Received;
            _methodDelegate = null;
            base.Dispose();
        }
    }
}
