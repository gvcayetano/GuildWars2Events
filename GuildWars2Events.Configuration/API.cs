//Credits: http://www.codeproject.com/Articles/37776/Writing-a-complex-custom-configuration-section

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildWars2Events.Configuration
{
    public class API : ConfigurationElement, IAPIUrl
    {
        /// <summary>
        /// Name of the API Uri
        /// </summary>
        [ConfigurationProperty("name")]
        public String Name 
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Uri path of the API
        /// </summary>
        [ConfigurationProperty("uri")]
        public String Uri
        {
            get { return (string)this["uri"]; }
            set { this["uri"] = value; }
        }
    }
}
