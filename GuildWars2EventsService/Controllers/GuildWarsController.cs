using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;
using Enyim.Caching.Memcached;
using GuildWars2EventsService.Info;

namespace GuildWars2EventsService.Controllers
{
    public class GuildWarsController
    {
        private DataContractJsonSerializer _dataContractJsonSerializer;
        private readonly JavaScriptSerializer _javaScriptSerializer;
        private const string EventsUrl = "https://api.guildwars2.com/v1/events.json?world_id=";
        private const string WorldsUrl = "https://api.guildwars2.com/v1/world_names.json";
        private const string EventNames = "https://api.guildwars2.com/v1/event_names.json";
        private const string MapNames = "https://api.guildwars2.com/v1/map_names.json";
        private const string WorldKey = "worlds";
        private const string MapKey = "maps";
        private const string EventNamesKey = "eventNames";

        public GuildWarsController()
        {
            _javaScriptSerializer = new JavaScriptSerializer();
        }

        public bool Update()
        {
            List<World> worlds = GetWorlds();
            foreach (World world in worlds)
            {
                Console.WriteLine("{0}{1}", EventsUrl, world.id);
            }
            return true;
        }

        private List<World> GetWorlds()
        {
            var worlds = CouchbaseManager.Instance.Get(WorldKey);
            if (worlds == null)
            {
                worlds = SubmitQuery(WorldsUrl);
                bool stored = CouchbaseManager.Instance.Store(StoreMode.Set, WorldKey, worlds);
            }
            return _javaScriptSerializer.Deserialize<List<World>>(worlds.ToString());
        }

        private object SubmitQuery<T>(string value)
        {
            _dataContractJsonSerializer = new DataContractJsonSerializer(typeof(List<T>));
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(value);
            httpWebRequest.MaximumAutomaticRedirections = 10;
            httpWebRequest.MaximumResponseHeadersLength = 10;
            httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            return receiveStream != null ? _dataContractJsonSerializer.ReadObject(receiveStream) : null;
        }

        private static string SubmitQuery(string value)
        {
            Console.WriteLine("Requesting: {0}", value);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(value);
            httpWebRequest.MaximumAutomaticRedirections = 10;
            httpWebRequest.MaximumResponseHeadersLength = 10;
            httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            if (receiveStream == null)
            {
                return string.Empty;
            }
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            return readStream.ReadToEnd();
        }
    }
}
