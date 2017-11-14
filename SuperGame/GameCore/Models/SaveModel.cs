using System;
using System.Collections.Generic;
using GameCore.Objects;

namespace GameCore.Models
{
    public class SaveModel
    {
        public List<MapObject> Objects { get; set; }

        public DateTime Date { get; set; }
    }
}