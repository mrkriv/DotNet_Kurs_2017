using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegaBot.Data
{
    [Serializable]
    public class Day
    {
        public Dictionary<int, Lesson> Lessons { get; set; }

        public Day()
        {
            Lessons = new Dictionary<int, Lesson>();
        }
    }
}