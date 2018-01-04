using Enterprise.IntegrationPatterns.Converters;
using Enterprise.IntegrationPatterns.Messages;
using NUnit.Framework;
using System;
using System.Threading;
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
        public void CreateSenderAndReceiver_IntegrationBrokerCorrect_MessagePassed()
        {
            using (var sender = new Sender<string>(_factory, _converter, "integrationBroker", "integrationTestQueue"))
            {
                sender.Send("Hello");
            }

            var worker = new Worker();
            using (var receiver = new Receiver<string>(worker, _factory, _converter, "integrationBroker", "integrationTestQueue"))
            {
                receiver.StartListening();
                Thread.Sleep(1000);

                if (string.IsNullOrEmpty(worker.MessageReceived))
                    throw new TimeoutException();

                Assert.AreEqual("Hello", worker.MessageReceived);
            }
        }

        [Test]
        public void WrokloadBalancer_CofigCorrect_LoadBalanced()
        {
            using (var sender = new Sender<string>(_factory, _converter, "integrationBroker", "worloadTestQueue"))
            {
                for (int i = 0; i < 500; i++)
                {
                    sender.Send("Hello" + i);
                }
            }

            Task<int> t1 = new Task<int>(CreateReceiver);
            Task<int> t2 = new Task<int>(CreateReceiver);
            t1.Start();
            t2.Start();
            t1.Wait();
            t2.Wait();

            Assert.GreaterOrEqual(t1.Result,10);
            Assert.GreaterOrEqual(t2.Result, 10);
        }

        [Test]
        public void PublishSubscribe_ConfigCorrect_MessagesPassed()
        {
            using (var sender = new Sender<string>(_factory, _converter, "integrationBroker", "publishTestExchange"))
            {
                sender.Send("Hello");
            }

            var worker = new Worker();
            using (var receiver = new Receiver<string>(worker, _factory, _converter, "integrationBroker", "subscriberOneTestQueue"))
            {
                receiver.StartListening();
                Thread.Sleep(1000);
            }

            Assert.AreEqual("Hello", worker.MessageReceived);
            Assert.AreEqual(1, worker.MessagesCount);

            using (var receiver = new Receiver<string>(worker, _factory, _converter, "integrationBroker", "subscriberTwoTestQueue"))
            {
                receiver.StartListening();
                Thread.Sleep(1000);
            }

            Assert.AreEqual("Hello", worker.MessageReceived);
            Assert.AreEqual(2, worker.MessagesCount);
        }

        class Worker : IWorker<string>
        {
            public event EventHandler<AckEventArgs> JobDone;
            public event EventHandler<NackEventArgs> JobFailed;
            public string MessageReceived { get; set; }
            public int MessagesCount { get; set; }
            public void DoJob(Message<string> message)
            {
                MessageReceived = message.Body;
                MessagesCount++;
                JobDone(this, new AckEventArgs() { MultipleAck = false, DeliveryTag = message.DeliveryTag });
            }
        }

        private int CreateReceiver()
        {
            var worker = new Worker();
            using (var receiver = new Receiver<string>(worker, _factory, _converter, "integrationBroker", "worloadTestQueue"))
            {
                receiver.StartListening();
                Thread.Sleep(1000);
            }

            return worker.MessagesCount;
        }
    }
}
