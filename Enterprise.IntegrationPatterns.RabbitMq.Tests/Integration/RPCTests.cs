using Enterprise.IntegrationPatterns.Converters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.IntegrationPatterns.RabbitMq.Tests.Integration
{
    [TestFixture]
    public class RPCTests
    {
        ChannelFactory _factory = new ChannelFactory();
        IMessageConverter _converter = new JsonMessageConverter();

        [Test]
        public void PingMehodCalled_RturnsArgument()
        {
            using (var server = new RPCServer<string, string>(_factory, _converter, "integrationBroker", "rpcTestQueue"))
            {
                server.Start(Ping);
                using (var client = new RPCClient<string, string>(_factory, _converter, "integrationBroker", "rpcTestQueue"))
                {
                    var result = client.Call("test");

                    Assert.AreEqual("test", result);
                }
            }
        }

        [Test]
        public void ExceptionMehodCalled_ReturnsException()
        {
            using (var server = new RPCServer<string, string>(_factory, _converter, "integrationBroker", "rpcTestQueue"))
            {
                server.Start(Exception);
                using (var client = new RPCClient<string, string>(_factory, _converter, "integrationBroker", "rpcTestQueue"))
                {
                    Assert.Throws<Exception>(() => client.Call("test"));
                }
            }
        }

        public string Ping(string argument)
        {
            return argument;
        }

        public string Exception(string argument)
        {
            throw new Exception("Test");
        }
    }
}
