using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegaBot.Model
{
    public class Chat
    {
        [JsonProperty("id")]
        public int ChatID { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
