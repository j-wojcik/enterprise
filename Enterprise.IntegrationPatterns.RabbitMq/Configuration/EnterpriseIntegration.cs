using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.IntegrationPatterns.RabbitMq.Configuration
{
    public class EnterpriseIntegration : ConfigurationSection
    {
        [ConfigurationProperty("brokers")]
        [ConfigurationCollection(typeof(Brokers))]
        public Brokers Brokers
        {
            get
            {
                return (Brokers)this["brokers"];
            }
            set
            {
                this["brokers"] = value;
            }
        }

        [ConfigurationProperty("exchanges")]
        [ConfigurationCollection(typeof(Exchanges))]
        public Exchanges Exchanges
        {
            get
            {
                return (Exchanges)this["exchanges"];
            }
            set
            {
                this["exchanges"] = value;
            }
        }

        [ConfigurationProperty("queues")]
        [ConfigurationCollection(typeof(Queues))]
        public Queues Queues
        {
            get
            {
                return (Queues)this["queues"];
            }
            set
            {
                this["queues"] = value;
            }
        }
    }
}
