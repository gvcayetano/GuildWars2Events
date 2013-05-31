//Credits: http://www.codeproject.com/Articles/37776/Writing-a-complex-custom-configuration-section

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildWars2Events.Configuration
{
    public class APIConfigurationSection : ConfigurationSection
    {
        API _element;
        public APIConfigurationSection()
        {
            _element = new API();
        }

        [ConfigurationProperty("apis", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(APICollection), 
            AddItemName = "api",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public APICollection Elements
        {
            get
            {
                return (APICollection)base["apis"];
            }
        }

        internal List<API> GetAPIs(string configSection)
        {
            List<API> apis = new List<API>();

            APIConfigurationSection myCustomSection = (APIConfigurationSection)ConfigurationManager.GetSection(configSection);

            foreach (API element in myCustomSection.Elements)
            {
                if (!string.IsNullOrEmpty(element.Name))
                {
                    apis.Add(element);
                }
            }

            return apis;
        }
    }
}
