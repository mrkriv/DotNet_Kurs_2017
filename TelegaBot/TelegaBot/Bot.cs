using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TelegaBot.Model;
using System.Reflection;
using System.Text.RegularExpressions;
using TelegaBot.Controller;
using TelegaBot.Data;

namespace TelegaBot
{
    public class Bot
    {
        public BotAPI API { get; set; }
        public DBContext DB { get; set; }
        public bool IsEnable { get; set; }

        public Bot(string ApiKey)
        {
            DB = DBContext.Load();
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

                if (FaidAction(update, type))
                    return;
            }

            API.SendMessage(update.Message.Chat, PrintHelp());
        }

        private string PrintHelp()
        {
            var sb = new StringBuilder();

            foreach (var type in typeof(ControllerBase).Assembly.GetTypes())
            {
                if (type.BaseType != typeof(ControllerBase))
                    continue;

                sb.AppendLine(PrintHelpController(type));
            }

            return sb.ToString();
        }

        private string PrintHelpController(Type controller)
        {
            var sb = new StringBuilder();

            foreach (var method in controller.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var maskAttr = method.GetCustomAttribute<MessageMaskAttribute>();
                var descAttr = method.GetCustomAttribute<DescriptionAttribute>();

                if (maskAttr != null && descAttr != null)
                {
                    var regx = new Regex(@"\([^\)]+\)");

                    var indexCounter = 0;
                    var pattern = regx.Replace(maskAttr.Pattern, m => ReplaceParam(m, method, indexCounter++));
                    pattern = pattern.Replace("\\s", " ");

                    sb.AppendLine(descAttr.Desc + ":\t" + pattern);
                }
            }

            return sb.ToString();
        }

        private string ReplaceParam(Match match, MethodInfo method, int index)
        {
            var param = method.GetParameters()[index];

            var descAttr = param.GetCustomAttribute<DescriptionAttribute>();

            return $"[{descAttr.Desc}]";
        }

        private bool FaidAction(Update update, Type type)
        {
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var maskAttr = method.GetCustomAttribute<MessageMaskAttribute>();

                if (maskAttr != null)
                {
                    var match = maskAttr.Mask.Match(update.Message.Text);

                    if (match.Success)
                    {
                        InvokeAction(update, type, match, method);
                        return true;
                    }
                }
            }
            return false;
        }
        private void InvokeAction(Update update, Type type, Match match, MethodInfo method)
        {
            var controller = (ControllerBase) Activator.CreateInstance(type);
            controller.Bind(update, API, DB);

            var param = new List<object>();

            for (var i = 1; i < match.Groups.Count; i++)
            {
                var t = method.GetParameters()[i - 1].ParameterType;
                var val = Convert.ChangeType(match.Groups[i].Value, t);
                param.Add(val);
            }

            method.Invoke(controller, param.ToArray());
        }
    }
}
