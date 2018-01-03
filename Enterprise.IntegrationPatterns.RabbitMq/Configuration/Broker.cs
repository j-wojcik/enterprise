using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.IntegrationPatterns.RabbitMq.Configuration
{
    public class Broker : BaseConfigurationElement
    {
        [ConfigurationProperty("address", DefaultValue = "", IsRequired = true)]
        public string Address
        {
            get
            {
                return (String)this["address"];
            }

            set
            {
                this["address"] = value;
            }
        }

        [ConfigurationProperty("virtualHost", DefaultValue = "", IsRequired = true)]
        public string VirtualHost
        {
            get
            {
                return (String)this["virtualHost"];
            }

            set
            {
                this["virtualHost"] = value;
            }
        }

        [ConfigurationProperty("username", DefaultValue = "", IsRequired = true)]
        public string Username
        {
            get
            {
                return (String)this["username"];
            }

            set
            {
                this["username"] = value;
            }
        }

        [ConfigurationProperty("password", DefaultValue = "", IsRequired = true)]
        public string Password
        {
            get
            {
                return (String)this["password"];
            }

            set
            {
                this["password"] = value;
            }
        }
    }
}
