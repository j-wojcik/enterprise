﻿using Enterprise.IntegrationPatterns.Messages;
using Enterprise.IntegrationPatterns.RabbitMq.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace Enterprise.IntegrationPatterns.RabbitMq
{
    public class Receiver<TMessage> : ChannelEndpoint<TMessage>, IReceiver
    {
        private readonly IWorker<TMessage> _worker;
        private EventingBasicConsumer _consumer;
        public Receiver(IWorker<TMessage> worker, IChannelFactory channelFactory, IMessageConverter messageConverter, string broker, string endpoint) 
            : base(channelFactory, messageConverter, broker, endpoint)
        {
            _worker = worker ?? throw new ArgumentException(nameof(worker));
            worker.JobDone += Worker_JobDone;
            worker.JobFailed += Worker_JobFailed;
            _consumer = new EventingBasicConsumer(_channel.Model);
        }

        private void Worker_JobFailed(object sender, NackEventArgs e)
        {
            _channel.Model.BasicNack(e.DeliveryTag, e.MultipleAck, e.Requeue);
        }

        private void Worker_JobDone(object sender, AckEventArgs e)
        {
            _channel.Model.BasicAck(e.DeliveryTag, e.MultipleAck);
        }

        public void StartListening()
        {  
            _consumer.Received += Consumer_Received;
            _channel.Model.BasicConsume(queue: _channel.EndpointName,
                                 autoAck: true,
                                 consumer: _consumer);
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var message = _messageConverter.Deserialize<TMessage>(e.Body);
            _worker.DoJob(new Message<TMessage>() { Body = message, DeliveryTag = e.DeliveryTag });
        }

        public override void Dispose()
        {
            _consumer.Received -= Consumer_Received;
            base.Dispose();
        }
    }
}
