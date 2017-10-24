using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TelegaBot
{
    public class MessageMaskAttribute : Attribute
    {
        public Regex Mask { get; set; }

        public MessageMaskAttribute(string pattern, RegexOptions RegexOptions = RegexOptions.None)
        {
            Mask = new Regex(pattern, RegexOptions);
        }
    }
}
