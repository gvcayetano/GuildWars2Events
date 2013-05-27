using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GuildWars2Events.Controllers;
using GuildWars2Events.Model;
using Newtonsoft.Json;

namespace GuildWars2Events.Web.APIs
{
    public class WorldsController : ApiController
    {
        private List<World> _worlds;

        // GET api/worlds
        public object Get()
        {
            var worlds = CouchbaseManager.Instance.Get(CouchbaseKeys.WorldNamesEn);
            _worlds = JsonConvert.DeserializeObject<List<World>>(worlds.ToString());
            return _worlds;
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
