using System;
using System.Configuration;
using Couchbase;
using Couchbase.Configuration;
using Couchbase.Management;

namespace GuildWars2EventsService.Controllers
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

    }
}
