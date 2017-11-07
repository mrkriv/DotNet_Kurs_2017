using System;

namespace TelegaBot.Data
{
    [Serializable]
    public class Lesson
    {
        public string Name { get; set; }
        public int Cabinet { get; set; }
    }
}