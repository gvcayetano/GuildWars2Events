//Credits: http://www.codeproject.com/Articles/37776/Writing-a-complex-custom-configuration-section

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildWars2Events.Configuration
{
    public class APICollection : ConfigurationElementCollection
    {
        public APICollection()
        {
            API myElement = (API)CreateNewElement();
            Add(myElement);
        }

        public void Add(API API)
        {
            BaseAdd(API);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            base.BaseAdd(element, false);
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new API();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((API)element).Name;
        }

        public API this[int Index]
        {
            get
            {
                return (API)BaseGet(Index);
            }
            set
            {
                if (BaseGet(Index) != null)
                {
                    BaseRemoveAt(Index);
                }
                BaseAdd(Index, value);
            }
        }

        new public API this[string Name]
        {
            get
            {
                return (API)BaseGet(Name);
            }
        }

        public int indexof(API element)
        {
            return BaseIndexOf(element);
        }

        public void Remove(API url)
        {
            if (BaseIndexOf(url) >= 0)
                BaseRemove(url.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }
    }
}
