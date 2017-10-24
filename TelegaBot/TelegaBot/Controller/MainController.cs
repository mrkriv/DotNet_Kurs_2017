using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TelegaBot.Controller
{
    public class MainController : ControllerBase
    {
        [MessageMask("hello", RegexOptions.IgnoreCase)]
        public void Hello()
        {
            var msg = "Hello " + Update.Message.FromUser.FirstName + "!!!";
            API.SendMessage(Update.Message.Chat, msg);
        }

        [MessageMask(@"\d+")]
        public void Test()
        {
            Console.WriteLine("test!");
        }

        public void TiBot()
        {
            Console.WriteLine("qwe");
        }
    }
}
