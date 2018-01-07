using Enterprise.IntegrationPatterns.RabbitMq.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Enterprise.IntegrationPatterns.RabbitMq
{
    public class RPCClient<TArgument, TReturn> : ChannelEndpoint<TArgument>
    {
        private static int _delay = 20;
        private readonly string _replyQueueName;
        private readonly EventingBasicConsumer _consumer;
        private readonly ConcurrentDictionary<string, TReturn> _responses = new ConcurrentDictionary<string, TReturn>();
        private readonly ConcurrentDictionary<string, Exception> _exceptions = new ConcurrentDictionary<string, Exception>();
        private ConcurrentDictionary<string, IBasicProperties> _properiesDictionary = new ConcurrentDictionary<string, IBasicProperties>();

        public RPCClient(IChannelFactory channelFactory, IMessageConverter messageConverter, string broker, string endpoint) : 
            base(channelFactory, messageConverter, broker, endpoint)
        {
            _replyQueueName = _channel.Model.QueueDeclare().QueueName;
            _consumer = new EventingBasicConsumer(_channel.Model);
            _consumer.Received += _consumer_Received;
            _channel.Model.BasicConsume(queue: _replyQueueName,
                     autoAck: true,
                     consumer: _consumer); 
        }

        private void _consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            if (_properiesDictionary.ContainsKey(e.BasicProperties.CorrelationId))
            {
                try
                {
                    var message = _messageConverter.Deserialize<TReturn>(e.Body);
                    _responses[e.BasicProperties.CorrelationId] = message;
                }
                catch(Exception ex)
                {
                    var message = _messageConverter.Deserialize<Exception>(e.Body);
                    _exceptions[e.BasicProperties.CorrelationId] = message;
                }
            }
        }

        public TReturn Call(TArgument argument)
        {
            var body = _messageConverter.Serialize(argument);

            var properies = _channel.Model.CreateBasicProperties();
            properies.CorrelationId = Guid.NewGuid().ToString();
            properies.ReplyTo = _replyQueueName;
            _properiesDictionary[properies.CorrelationId]  = properies;

            _channel.Model.BasicPublish(exchange: _channel.IsExchange ? _channel.EndpointName : "",
                        routingKey: _channel.IsExchange ? "":_channel.EndpointName,
                        basicProperties: properies,
                        body: body);

            while (true)
            {
                if (_responses.ContainsKey(properies.CorrelationId))
                {
                    TReturn response;
                    _responses.TryRemove(properies.CorrelationId, out response);
                    return response;
                }

                if(_exceptions.ContainsKey(properies.CorrelationId))
                {
                    Exception exception;
                    _exceptions.TryRemove(properies.CorrelationId, out exception);
                    throw exception;
                }

                Thread.Sleep(_delay);
            }
        }

        public override void Dispose()
        {
            _consumer.Received -= _consumer_Received;
            base.Dispose();
        }
    }
}
