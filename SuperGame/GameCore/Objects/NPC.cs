using System;
using System.Numerics;
using GameCore.Components;

namespace GameCore.Objects
{
    public class NPC : Character
    {
        public AI Intelect { get; set; }

        public override void OnAttachToWorld()
        {
            base.OnAttachToWorld();
            Intelect = new AI(this);
        }

        public override void OnTick(float dt)
        {
            Intelect.OnTick(dt);
            base.OnTick(dt);
        }
    }
}