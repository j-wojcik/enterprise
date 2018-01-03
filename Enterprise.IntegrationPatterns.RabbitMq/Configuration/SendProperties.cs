using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.IntegrationPatterns.RabbitMq.Configuration
{
    public class SendProperties : BaseConfigurationElement
    {
        [ConfigurationProperty("address", DefaultValue = "", IsRequired = false)]
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

        [ConfigurationProperty("timestamp", DefaultValue = "", IsRequired = false)]
        public DateTime Timestamp
        {
            get
            {
                return (DateTime)this["timestamp"];
            }

            set
            {
                this["timestamp"] = value;
            }
        }

        [ConfigurationProperty("replyToAddress", DefaultValue = "", IsRequired = false)]
        public string ReplyToAddress
        {
            get
            {
                return (string)this["replyToAddress"];
            }

            set
            {
                this["replyToAddress"] = value;
            }
        }

        [ConfigurationProperty("replyTo", DefaultValue = "", IsRequired = false)]
        public string ReplyTo
        {
            get
            {
                return (string)this["replyTo"];
            }

            set
            {
                this["replyTo"] = value;
            }
        }

        [ConfigurationProperty("priority", DefaultValue = "", IsRequired = false)]
        public byte Priority
        {
            get
            {
                return (byte)this["priority"];
            }

            set
            {
                this["priority"] = value;
            }
        }

        [ConfigurationProperty("persistent", DefaultValue = "", IsRequired = false)]
        public bool Persistent
        {
            get
            {
                return (bool)this["persistent"];
            }

            set
            {
                this["persistent"] = value;
            }
        }

        [ConfigurationProperty("messageId", DefaultValue = "", IsRequired = false)]
        public string MessageId
        {
            get
            {
                return (string)this["messageId"];
            }

            set
            {
                this["messageId"] = value;
            }
        }

        [ConfigurationProperty("expiration", DefaultValue = "", IsRequired = false)]
        public string Expiration
        {
            get
            {
                return (string)this["expiration"];
            }

            set
            {
                this["expiration"] = value;
            }
        }

        [ConfigurationProperty("deliveryMode", DefaultValue = "", IsRequired = false)]
        public byte DeliveryMode
        {
            get
            {
                return (byte)this["deliveryMode"];
            }

            set
            {
                this["deliveryMode"] = value;
            }
        }

        [ConfigurationProperty("correlationId", DefaultValue = "", IsRequired = false)]
        public string CorrelationId
        {
            get
            {
                return (string)this["correlationId"];
            }

            set
            {
                this["correlationId"] = value;
            }
        }

        [ConfigurationProperty("contentType", DefaultValue = "", IsRequired = false)]
        public string ContentType
        {
            get
            {
                return (string)this["contentType"];
            }

            set
            {
                this["contentType"] = value;
            }
        }

        [ConfigurationProperty("contentEncoding", DefaultValue = "", IsRequired = false)]
        public string ContentEncoding
        {
            get
            {
                return (string)this["contentEncoding"];
            }

            set
            {
                this["contentEncoding"] = value;
            }
        }

        [ConfigurationProperty("clusterId", DefaultValue = "", IsRequired = false)]
        public string ClusterId
        {
            get
            {
                return (string)this["clusterId"];
            }

            set
            {
                this["clusterId"] = value;
            }
        }

        [ConfigurationProperty("appId", DefaultValue = "", IsRequired = false)]
        public string AppId
        {
            get
            {
                return (string)this["appId"];
            }

            set
            {
                this["appId"] = value;
            }
        }

        [ConfigurationProperty("type", DefaultValue = "", IsRequired = false)]
        public string Type
        {
            get
            {
                return (string)this["type"];
            }

            set
            {
                this["type"] = value;
            }
        }
    }
}
