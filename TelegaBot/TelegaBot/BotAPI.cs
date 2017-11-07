using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TelegaBot.Model;

namespace TelegaBot
{
    public class BotAPI
    {
        public string Key { get; set; }

        public BotAPI(string ApiKey)
        {
            Key = ApiKey;
        }

        public UpdateResult GetUpdates(int maxUpdateID)
        {
            var url = string.Format("https://api.telegram.org/bot{0}/getUpdates?offset={1}", Key, maxUpdateID);
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Timeout = 500;

            using (var response = (HttpWebResponse)request.GetResponseAsync().Result)
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var data = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<UpdateResult>(data);
            }
        }

        public void SendMessage(Chat TargetChat, string Message)
        {
            var url = string.Format("https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}",
                Key, TargetChat.ChatID, Message);

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.GetResponseAsync();
        }
    }
}