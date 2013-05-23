using System;
using Newtonsoft.Json;

namespace GuildWars2EventsService.Info
{
    [Serializable]
    public class World
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
