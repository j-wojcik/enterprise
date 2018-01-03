using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.IntegrationPatterns.RabbitMq.Configuration
{
    public class BaseConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("key", DefaultValue = "", IsRequired = true, IsKey =true)]
        public string Key
        {
            get
            {
                return (String)this["key"];
            }

            set
            {
                this["key"] = value;
            }
        }
    }
}
