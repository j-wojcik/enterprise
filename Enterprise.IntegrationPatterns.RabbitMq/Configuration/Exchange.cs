using Enterprise.IntegrationPatterns.RabbitMq.Configuration.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.IntegrationPatterns.RabbitMq.Configuration
{
    public class Exchange : BaseConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "", IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }

            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("type", DefaultValue = "fanout", IsRequired = true)]
        public ExchangeType ExchangeType
        {
            get
            {
                return (ExchangeType)this["type"];
            }

            set
            {
                this["type"] = value;
            }
        }


        [ConfigurationProperty("routingKey", DefaultValue = "", IsRequired = false)]
        public string RoutingKey
        {
            get
            {
                return (string)this["routingKey"];
            }

            set
            {
                this["routingKey"] = value;
            }
        }
    }
}
