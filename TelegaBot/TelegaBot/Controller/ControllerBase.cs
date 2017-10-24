using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegaBot.Model;

namespace TelegaBot.Controller
{
    public abstract class ControllerBase
    {
        protected Update Update { get; private set; }
        protected BotAPI API { get; private set; }

        public void Bind(Update update, BotAPI api)
        {
            Update = update;
            API = api;
        }
    }
}