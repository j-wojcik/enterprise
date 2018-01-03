using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.IntegrationPatterns.RabbitMq.Configuration.Enums
{
    public enum ExchangeType
    {
        direct = 0,
        fanout = 1,
        topic = 2,
        headers = 3
    }
}
