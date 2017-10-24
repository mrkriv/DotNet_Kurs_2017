using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TelegaBot.Model
{
    public class UpdateResult
    {
        [JsonProperty("ok")]
        public bool OK { get; set; }

        [JsonProperty("result")]
        public List<Update> Result { get; set; }
    }
}
