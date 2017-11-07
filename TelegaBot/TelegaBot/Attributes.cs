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
        public string Pattern { get; set; }

        public MessageMaskAttribute(string pattern, RegexOptions regexOptions = RegexOptions.None)
        {
            Mask = new Regex(pattern, regexOptions);
            Pattern = pattern;
        }
    }
    public class DescriptionAttribute : Attribute
    {
        public string Desc { get; set; }

        public DescriptionAttribute(string description)
        {
            Desc = description;
        }
    }
}
