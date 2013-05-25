using GuildWars2Events.Model.Extensions;

namespace GuildWars2Events.Model
{
    public class GuildWars2ApiHandler
    {

        private const string EventsUrl = "https://api.guildwars2.com/v1/events.json?world_id=";
        private const string WorldsUrl = "https://api.guildwars2.com/v1/world_names.json";
        private const string EventNamesUrl = "https://api.guildwars2.com/v1/event_names.json";
        private const string MapNamesUrl = "https://api.guildwars2.com/v1/map_names.json";

        public GuildWars2ApiHandler()
        {
            
        }

        public string GetMapNames()
        {
            return MapNamesUrl.SubmitQuery();
        }

        public string GetEventNames()
        {
            return EventNamesUrl.SubmitQuery();
        }

        public string GetWorlds()
        {
            return WorldsUrl.SubmitQuery();
        }

        public string GetEvent(World world)
        {
            return string.Format("{0}{1}", EventsUrl, world.id).SubmitQuery();
        }
    }
}
