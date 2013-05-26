using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildWars2Events.Model
{
    public static class CouchbaseKeys
    {
        public const string ActiveWorlds = "ActiveWorlds";

        public const string EventNamesEn = "EventNames_EN";
        public const string EventNamesFr = "EventNames_FR";
        public const string EventNamesDe = "EventNames_DE";
        public const string EventNamesEs = "EventNames_ES";

        public const string MapNamesEn = "MapNames_EN";
        public const string MapNamesFr = "MapNames_FR";
        public const string MapNamesDe = "MapNames_DE";
        public const string MapNamesEs = "MapNames_ES";

        public const string ServiceState = "ServiceState";

        public const string WorldNamesEn = "WolrdNames_EN";
        public const string WorldNamesFr = "WolrdNames_FR";
        public const string WorldNamesDe = "WolrdNames_DE";
        public const string WorldNamesEs = "WolrdNames_ES";

        public static string GenerateKeyForWorld(World world)
        {
            return string.Format("World_{0}", world.id);
        }
    }
}