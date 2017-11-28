using System.Numerics;
using GameCore.Objects;

namespace GameCore.Models
{
    public class PhysicsModel
    {
        public float Radius { get; set; }
        public bool IsSatatic { get; set; }
        public MapObject MapObject { get; set; }
    }
}