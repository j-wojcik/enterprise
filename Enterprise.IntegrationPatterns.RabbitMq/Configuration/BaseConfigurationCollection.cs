using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.IntegrationPatterns.RabbitMq.Configuration
{
    public class BaseConfigurationCollection<TElement> : ConfigurationElementCollection where TElement : BaseConfigurationElement, new()
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new TElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TElement)element).Key;
        }

        new public TElement this[string Key]
        {
            get
            {
                return (TElement)BaseGet(Key);
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(ElementName, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        public TElement this[int idx]
        {
            get
            {
                return (TElement)BaseGet(idx);
            }
        }
    }
}
