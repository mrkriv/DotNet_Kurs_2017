using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegaBot.Model
{
    public class Update
    {
        [JsonProperty("update_id")]
        public int UpdateID { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }
    }
}
