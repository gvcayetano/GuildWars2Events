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
        public const string EventNames = "EventNames";
        public const string EventNamesFr = "EventNames_FR";
        public const string EventNamesDe = "EventNames_DE";
        public const string EventNamesEs = "EventNames_ES";
        public const string MapNames = "MapNames";
        public const string MapNamesFr = "MapNames_FR";
        public const string MapNamesDe = "MapNames_DE";
        public const string MapNamesEs = "MapNames_ES";
        public const string ServiceState = "ServiceState";
        public const string Worlds = "Worlds";

        public static string GenerateKeyForWorld(World world)
        {
            return string.Format("World_{0}", world.id);
        }
    }
}