using RabbitMQ.Client;

namespace Enterprise.IntegrationPatterns.RabbitMq.Common
{
    public class Channel
    {
        public IModel Model { get; set; }
        public bool IsExchange { get; set; }
        public string EndpointName { get; set; }
    }
}
