using System;

namespace GuildWars2Events.Model
{
    [Serializable]
    public class World
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime LastActive { get; set; }
    }
}
