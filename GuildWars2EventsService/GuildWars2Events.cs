using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization.Json;
using System.Timers;
using Enyim.Caching.Memcached;
using GuildWars2Events.Model;
using GuildWars2EventsService.Controllers;

namespace GuildWars2EventsService
{
    public class GuildWars2Events
    {
        readonly Timer _timer;
        private readonly int _timerInterval;

        public GuildWars2Events()
        {
            _timerInterval = Convert.ToInt32(ConfigurationManager.AppSettings["Interval"]);// Interval in Seconds
            _timer = new Timer(_timerInterval * 1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => GetEvents();
            GetEvents();
        }
        public void Start()
        {
            CouchbaseManager.SetServiceState(false);
            _timer.Start();
        }
        public void Stop()
        {
            CouchbaseManager.SetServiceState(false);
            _timer.Stop();
        }

        public void GetEvents()
        {
            UpdateInfo updateInfo = null;
            try
            {
                GuildWarsController guildWarsController = new GuildWarsController();
                updateInfo = guildWarsController.Update();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                CouchbaseManager.SetServiceState(false);
            }
            finally
            {
                if (updateInfo != null)
                {
                    if (!updateInfo.IsActive)
                    {
                        Console.WriteLine("[{0}] Number of active worlds: {1}.", DateTime.Now,
                                          updateInfo.ActiveWorldsCount);
                    }
                    Console.WriteLine(updateInfo.Message);
                }
            }
        }
    }
}
