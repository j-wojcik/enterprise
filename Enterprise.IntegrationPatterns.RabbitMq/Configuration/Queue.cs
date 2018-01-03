using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.IntegrationPatterns.RabbitMq.Configuration
{
    public class Queue : BaseConfigurationElement
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

        [ConfigurationProperty("durable", DefaultValue = "false", IsRequired = false)]
        public bool Durable
        {
            get
            {
                return (bool)this["durable"];
            }

            set
            {
                this["durable"] = value;
            }
        }

        [ConfigurationProperty("exclusive", DefaultValue = "false", IsRequired = false)]
        public bool Exclusive
        {
            get
            {
                return (bool)this["exclusive"];
            }

            set
            {
                this["exclusive"] = value;
            }
        }

        [ConfigurationProperty("autoDelete", DefaultValue = "false", IsRequired = false)]
        public bool AutoDelete
        {
            get
            {
                return (bool)this["autoDelete"];
            }

            set
            {
                this["autoDelete"] = value;
            }
        }
    }
}
