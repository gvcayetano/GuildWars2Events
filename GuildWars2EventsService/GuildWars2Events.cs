using System;
using System.Configuration;
using System.Runtime.Serialization.Json;
using System.Timers;
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
        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }

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
            }
            finally
            {
                if (updateInfo != null)
                {
                    Console.WriteLine("[{0}] Number of active worlds: {1}.", DateTime.Now, updateInfo.ActiveWorldsCount);
                    if (updateInfo.ActiveWorldsUpdates > 0)
                    {
                        Console.WriteLine("[{0}] Done with all updates.", DateTime.Now);
                    }
                    else
                    {
                        Console.WriteLine("[{0}] No updates.", DateTime.Now);
                    }
                }
            }
        }
    }
}
