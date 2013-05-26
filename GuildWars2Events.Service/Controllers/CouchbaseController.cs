using System;
using System.Configuration;
using Couchbase;
using Couchbase.Configuration;
using Enyim.Caching.Memcached;
using GuildWars2Events.Model;
using Newtonsoft.Json;

namespace GuildWars2Events.Service.Controllers
{
    public class CouchbaseController
    {
    }

    public static class CouchbaseManager
    {
        private readonly static CouchbaseClient _instance;

        static CouchbaseManager()
        {
            CouchbaseClientConfiguration couchbaseClientConfiguration = new CouchbaseClientConfiguration()
            {
                Bucket = "default",
                BucketPassword = string.Empty
            };
            couchbaseClientConfiguration.Urls.Add(new Uri(ConfigurationManager.AppSettings["CouchBaseServer"]));
            _instance = new CouchbaseClient(couchbaseClientConfiguration);
        }

        public static CouchbaseClient Instance { get { return _instance; } }

        public static bool StoreObjectToJson(string key, object obj)
        {
            return Instance.Store(
                        StoreMode.Set
                        , key
                        , JsonConvert.SerializeObject(obj));
        }

        public static bool SetServiceState(bool isActive)
        {
            ServiceState serviceState = new ServiceState()
                {
                    IsActive = isActive
                };
            return Instance.Store(StoreMode.Set, CouchbaseKeys.ServiceState, JsonConvert.SerializeObject(serviceState));
        }

        public static ServiceState GetServiceState()
        {
            var serviceStateJson = Instance.Get(CouchbaseKeys.ServiceState);
            return JsonConvert.DeserializeObject<ServiceState>(serviceStateJson.ToString());
        }
    }
}
