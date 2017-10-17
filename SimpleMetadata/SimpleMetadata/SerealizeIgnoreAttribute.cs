using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SerealizeIgnoreAttribute : Attribute
    {
    }
}
