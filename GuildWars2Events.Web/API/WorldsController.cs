using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GuildWars2Events.Controllers;
using GuildWars2Events.Model;
using Newtonsoft.Json;

namespace GuildWars2Events.Web.API
{
    public class WorldsController : ApiController
    {
        private List<World> _worlds;

        // GET api/worlds
        public object Get()
        {
            return CouchbaseManager.GetWorlds(CouchbaseKeys.WorldNamesEn);
        }

        // GET api/worlds
        public object GetLocalizedWorlds(string lang)
        {
            string key;
            switch (lang.ToLower())
            {
                case "de":
                    {
                        key = CouchbaseKeys.WorldNamesDe;
                        break;
                    }
                case "es":
                    {
                        key = CouchbaseKeys.WorldNamesEs;
                        break;
                    }
                case "fr":
                    {
                        key = CouchbaseKeys.WorldNamesFr;
                        break;
                    }
                default:
                    {
                        key = CouchbaseKeys.WorldNamesEn;
                        break;
                    }
            }
            return CouchbaseManager.GetWorlds(key);
        }

        // GET api/worlds/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/worlds
        public void Post([FromBody]string value)
        {
        }

        // PUT api/worlds/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/worlds/5
        public void Delete(int id)
        {
        }
    }
}
