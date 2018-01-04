using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.IntegrationPatterns.Messages
{
    public class Message<TBody>
    {
        public TBody Body { get; set; }
        public ulong DeliveryTag { get; set; }

    }
}
