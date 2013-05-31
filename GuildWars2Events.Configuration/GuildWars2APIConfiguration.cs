using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildWars2Events.Configuration
{
    public class GuildWars2APIConfiguration : APIConfigurationSection
    {
        public GuildWars2APIConfiguration() : base()
        {
        }

        public List<API> GetAPIs()
        {
            return base.GetAPIs("guildWars2api");
        }
    }
}
