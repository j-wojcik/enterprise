using Enterprise.IntegrationPatterns.Converters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.IntegrationPatterns.RabbitMq.Tests
{
    [TestFixture]
    public class SenderTests
    {
        ChannelFactory _factory = new ChannelFactory();
        IMessageConverter _converter = new JsonMessageConverter();

        [Test]
        public void CreateSender_IntegrationBrokerDoesNotExists_ArgumentException()
        {
            
            Assert.Throws<ArgumentException>(() => new Sender<string>(_factory, _converter, "fooBroker", "integrationTestQueue"));
        }

        [Test]
        public void CreateSender_WrongAddress_ArgumentException()
        {
            Assert.Throws<RabbitMQ.Client.Exceptions.BrokerUnreachableException>(() => new Sender<string>(_factory, _converter, "configTestBroker", "integrationTestQueue"));
        }

        [Test]
        public void CreateSender_IntegrationBrokerCorrect_ConnectionEstablished()
        {
            using (var sender = new Sender<string>(_factory, _converter, "integrationBroker", "integrationTestQueue"))
            {
                sender.Send("Hello");
            }
        }
    }
}
