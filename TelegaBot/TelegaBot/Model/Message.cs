using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelegaBot.Model
{
    public class Message
    {
        [JsonProperty("message_id")]
        public int MessageID { get; set; }

        [JsonProperty("from")]
        public User FromUser { get; set; }

        [JsonProperty("chat")]
        public Chat Chat { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}