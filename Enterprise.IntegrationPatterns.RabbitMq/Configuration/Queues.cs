using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.IntegrationPatterns.RabbitMq.Configuration
{
    public class Queues : BaseConfigurationCollection<Queue>
    {
        protected override string ElementName => "queue";
    }
}
