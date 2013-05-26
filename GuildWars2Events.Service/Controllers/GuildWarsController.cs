using System;
using System.Collections.Generic;
using System.Configuration;
using Enyim.Caching.Memcached;
using GuildWars2Events.Model;
using GuildWars2Events.Model.Extensions;
using Newtonsoft.Json;

namespace GuildWars2Events.Service.Controllers
{
    public class GuildWarsController
    {
        // Raw from arena net.
        private readonly List<World> _worlds;
        private readonly List<EventName> _eventNames;
        private readonly List<MapName> _mapNames;
        // Raw from arena net.

        private readonly GuildWars2ApiHandler _guildWars2ApiHandler;
        private readonly List<World> _activeWorlds;
        private readonly int _minutesBeforeAWorldBecomesInactive;

        public GuildWarsController()
        {
            _guildWars2ApiHandler = new GuildWars2ApiHandler();
            _minutesBeforeAWorldBecomesInactive = Convert.ToInt32(ConfigurationManager.AppSettings["MinutesBeforeAWorldBecomesInactive"]);
            if(_worlds == null) _worlds = GetWorlds();
            if (_eventNames == null) _eventNames = GetEventNames();
            if (_mapNames == null) _mapNames = GetMapNames();
            var activeWorlds = CouchbaseManager.Instance.Get(CouchbaseKeys.ActiveWorlds);
            if (activeWorlds != null)
            {
                _activeWorlds = JsonConvert.DeserializeObject<List<World>>(activeWorlds.ToString());
            }
            else
            {
                //If ActiveWorlds is not set make all worlds active for 1 minute.
                _activeWorlds = new List<World>();
                foreach (World world in _worlds)
                {
                    world.LastActive = DateTime.Now.AddMinutes(-(_minutesBeforeAWorldBecomesInactive-1));
                    _activeWorlds.Add(world);
                }
                if (_activeWorlds != null)
                {
                    CouchbaseManager.Instance.Store(
                        StoreMode.Set
                        , CouchbaseKeys.ActiveWorlds
                        , JsonConvert.SerializeObject(_activeWorlds));
                }
            }
        }

        private List<MapName> GetMapNames()
        {
            var mapNames = CouchbaseManager.Instance.Get(CouchbaseKeys.MapNames);
            if (mapNames == null)
            {
                mapNames = _guildWars2ApiHandler.GetMapNames();
                bool isStored = CouchbaseManager.Instance.Store(StoreMode.Set, CouchbaseKeys.MapNames, mapNames);
            }
            return JsonConvert.DeserializeObject<List<MapName>>(mapNames.ToString());
        }

        private List<EventName> GetEventNames()
        {
            var eventNames = CouchbaseManager.Instance.Get(CouchbaseKeys.EventNames);
            if (eventNames == null)
            {
                eventNames = _guildWars2ApiHandler.GetEventNames();
                bool isStored = CouchbaseManager.Instance.Store(StoreMode.Set, CouchbaseKeys.EventNames, eventNames);
            }
            return JsonConvert.DeserializeObject<List<EventName>>(eventNames.ToString());
        }

        private List<World> GetWorlds()
        {
            var worlds = CouchbaseManager.Instance.Get(CouchbaseKeys.Worlds);
            if (worlds == null)
            {
                worlds = _guildWars2ApiHandler.GetWorlds();
                bool isStored = CouchbaseManager.Instance.Store(StoreMode.Set, CouchbaseKeys.Worlds, worlds);
            }
            return JsonConvert.DeserializeObject<List<World>>(worlds.ToString());
        }

        public UpdateInfo Update()
        {
            UpdateInfo updateInfo = new UpdateInfo()
                {
                    ActiveWorldsCount = 0
                    , ActiveWorldsUpdates = 0
                    , Message = string.Format("[{0}] The previous update process is still active. Will try again later.", DateTime.Now)
                };
            ServiceState serviceState = CouchbaseManager.GetServiceState();
            if (serviceState.IsActive)
            {
                updateInfo.IsActive = serviceState.IsActive;
                return updateInfo;
            }
            CouchbaseManager.SetServiceState(true);
            if (_activeWorlds != null)
            {
                foreach (World world in _activeWorlds)
                {
                    //TODO need to make this Asynchronous
                    if (DateTime.Now - world.LastActive < new TimeSpan(0, 0, _minutesBeforeAWorldBecomesInactive, 0))
                    {
                        string key = CouchbaseKeys.GenerateKeyForWorld(world);
                        bool isStored = CouchbaseManager.Instance.Store(
                            StoreMode.Set
                            , key
                            , _guildWars2ApiHandler.GetEvent(world)
                            );
                        if (isStored)
                        {
                            updateInfo.ActiveWorldsUpdates++;
                            Console.WriteLine(
                                "[{0}] {1} Updated."
                                , DateTime.Now
                                , key);
                        }
                        updateInfo.ActiveWorldsCount++;
                    }
                }
            }
            updateInfo.Message = string.Format(updateInfo.ActiveWorldsUpdates == 0 ? "[{0}] No updates." : "[{0}] Done with all updates.", DateTime.Now);
            CouchbaseManager.SetServiceState(false);
            return updateInfo;
        }

    }
}
