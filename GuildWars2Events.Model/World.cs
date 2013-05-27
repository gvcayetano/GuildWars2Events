using System;
using System.Runtime.Serialization;

namespace GuildWars2Events.Model
{
    [DataContract]
    public class World
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public DateTime LastActive { get; set; }
    }
}
