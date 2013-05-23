using System;
using System.Configuration;
using System.Runtime.Serialization.Json;
using System.Timers;
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
            try
            {
                Console.WriteLine("It is {0} an all is well", DateTime.Now);
                GuildWarsController guildWarsController = new GuildWarsController();
                bool success = guildWarsController.Update();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
