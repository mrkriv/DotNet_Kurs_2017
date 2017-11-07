using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegaBot.Data
{
    [Serializable]
    public class TelegaUser
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Schedule Schedule { get; set; }
    }
}