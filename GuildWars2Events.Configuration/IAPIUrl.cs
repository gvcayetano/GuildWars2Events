using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildWars2Events.Configuration
{
    public interface IAPIUrl
    {
        String Name { get; set; }

        String Uri { get; set; }
    }
}
