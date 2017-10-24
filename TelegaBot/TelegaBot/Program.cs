using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TelegaBot.Model;

namespace TelegaBot
{
    class Program
    {
        private static readonly string ConfigFile = "setting.json";

        static void Main(string[] args)
        {
            Settings setting; 

            if (File.Exists(ConfigFile))
            {
                var data = File.ReadAllText(ConfigFile);
                setting = JsonConvert.DeserializeObject<Settings>(data);
            }
            else
            {
                setting = new Settings();
            }

            var bot = new Bot(setting.APIKey);

            bot.Begin();
        }
    }
}
