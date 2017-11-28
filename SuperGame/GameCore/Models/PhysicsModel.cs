using System;
using System.Numerics;
using GameCore.Objects;

namespace GameCore.Models
{
    public abstract class PhysicsModel
    {
        public bool IsSatatic { get; set; }
        public bool IsCollision { get; set; } = true;
        public MapObject MapObject { get; set; }

        public event Action<PhysicsModel, PhysicsModel> OnIntersection;

        public virtual void Intersection(PhysicsModel modelB)
        {
            
        }

        protected virtual void CallOnIntersection(PhysicsModel arg2)
        {
            OnIntersection?.Invoke(this, arg2);
        }
    }
}