using Enterprise.IntegrationPatterns.RabbitMq.Configuration;
using Enterprise.IntegrationPatterns.RabbitMq.Configuration.Enums;
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
    public class ConfigurationTests
    {
        [Test]
        public void ReadConfigSection_SectionExists_ReurnsCorrectType()
        {
            var configuration = ConfigurationManager.GetSection("EnterpriseIntegration");

            Assert.IsNotNull(configuration);
            Assert.IsAssignableFrom<EnterpriseIntegration>(configuration);
        }

        [Test]
        public void ReadConfigSection_BrokerAsDescribedInTest_PropertiesCorrec()
        {
            var configuration = ConfigurationManager.GetSection("EnterpriseIntegration") as EnterpriseIntegration;

            var testBroker = configuration.Brokers["configTestBroker"];
            Assert.IsNotNull(testBroker);
            Assert.IsAssignableFrom<Broker>(testBroker);
            Assert.AreEqual("configTestBroker", testBroker.Key);
            Assert.AreEqual("localhost", testBroker.Address);
            Assert.AreEqual("test", testBroker.VirtualHost);
            Assert.AreEqual("testUser", testBroker.Username);
            Assert.AreEqual("testPass", testBroker.Password);
        }

        [Test]
        public void ReadConfigSection_QueueAsDescribedInTest_PropertiesCorrect()
        {
            var configuration = ConfigurationManager.GetSection("EnterpriseIntegration") as EnterpriseIntegration;

            var testQueue = configuration.Queues["configTestQueue"];
            Assert.IsNotNull(testQueue);
            Assert.IsAssignableFrom<Queue>(testQueue);
            Assert.AreEqual("configTestQueue", testQueue.Key);
            Assert.AreEqual(true, testQueue.Durable);
            Assert.AreEqual(true, testQueue.Exclusive);
            Assert.AreEqual(true, testQueue.AutoDelete);
        }

        [Test]
        public void ReadConfigSection_ExchangeAsDescribedInTest_PropertiesCorrect()
        {
            var configuration = ConfigurationManager.GetSection("EnterpriseIntegration") as EnterpriseIntegration;

            var testExchange = configuration.Exchanges["configTestExchange"];
            Assert.IsNotNull(testExchange);
            Assert.IsAssignableFrom<Exchange>(testExchange);
            Assert.AreEqual("configTestExchange", testExchange.Key);
            Assert.AreEqual("test", testExchange.Name);
            Assert.AreEqual(ExchangeType.fanout , testExchange.ExchangeType);
            Assert.AreEqual(string.Empty, testExchange.RoutingKey);
            Assert.AreEqual(false, testExchange.Durable);
        }

        [Test]
        public void ReadConfigSection_QueueDefault_PropertiesCorrect()
        {
            var configuration = ConfigurationManager.GetSection("EnterpriseIntegration") as EnterpriseIntegration;

            var testQueue = configuration.Queues["configTestDefaultQueue"];
            Assert.IsNotNull(testQueue);
            Assert.IsAssignableFrom<Queue>(testQueue);
            Assert.AreEqual("configTestDefaultQueue", testQueue.Key);
            Assert.AreEqual(false, testQueue.Durable);
            Assert.AreEqual(false, testQueue.Exclusive);
            Assert.AreEqual(false, testQueue.AutoDelete);
        }
    }
}
