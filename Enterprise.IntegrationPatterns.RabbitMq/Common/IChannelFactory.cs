using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.IntegrationPatterns.RabbitMq.Common
{
    public interface IChannelFactory : IDisposable
    {
        IModel OpenChannel(string broker, string endpoint);
    }
}
