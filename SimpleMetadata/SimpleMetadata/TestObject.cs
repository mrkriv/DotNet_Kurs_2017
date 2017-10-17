using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication
{
    public class TestObject
    {
        [DisplayName("987")]
        public string Name { get; set; }

        [DisplayName("_test_")]
        [SerealizeIgnore]
        public int Number { get; set; }

        public float FloatNumber { get; set; }

        [DisplayName("qweqwe")]
        public List<string> StringList { get; set; }
    }
}
