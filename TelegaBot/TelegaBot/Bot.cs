using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TelegaBot.Model;
using System.Reflection;
using TelegaBot.Controller;

namespace TelegaBot
{
    public class Bot
    {
        public BotAPI API { get; set; }
        public bool IsEnable { get; set; }

        public Bot(string ApiKey)
        {
            API = new BotAPI(ApiKey);
        }

        public void Begin()
        {
            var maxUpdateID = 0;
            IsEnable = true;

            while (IsEnable)
            {
                var result = API.GetUpdates(maxUpdateID);

                if (result.Result.Count == 0)
                    continue;

                maxUpdateID = result.Result.Select(x => x.UpdateID).Max() + 1;

                foreach (var update in result.Result)
                {
                    ProcessUpdate(update);
                }

                Task.Delay(200).Wait();
            }
        }

        private void ProcessUpdate(Update update)
        {
            Console.WriteLine("{0}: {1}", update.Message.FromUser.FirstName, update.Message.Text);
            //Console.WriteLine($"{update.Message.From.FirstName}: {update.Message.Text}"); // C# 7.0

            foreach (var type in typeof(ControllerBase).Assembly.GetTypes())
            {
                if (type.BaseType != typeof(ControllerBase))
                    continue;

                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    var maskAttr = method.GetCustomAttribute<MessageMaskAttribute>();

                    if (maskAttr != null && maskAttr.Mask.IsMatch(update.Message.Text))
                    {
                        var controller = (ControllerBase)Activator.CreateInstance(type);
                        controller.Bind(update, API);

                        method.Invoke(controller, new object[] { });
                        
                        return;
                    }
                }
            }
        }
    }
}
