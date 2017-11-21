using System.Collections.Generic;
using System.Numerics;
using GameCore.Render;

namespace GameCore.Objects
{
    public class Character : MapObject
    {
        public Dictionary<string, Animation> Animations;

        public float Speed { get; set; }
        public Vector2 Velocity { get; set; }

        public Character()
        {
            Animations = new Dictionary<string, Animation>
            {
                {
                    "idle", new Animation
                    {
                        PathMask = "player\\idle_{0}",
                        ImageCount = 4,
                        Speed = 3
                    }
                },
                {
                    "walk", new Animation
                    {
                        PathMask = "player\\walk_{0}",
                        ImageCount = 4,
                        Speed = .2f
                    }
                },
                {
                    "run", new Animation
                    {
                        PathMask = "player\\run_{0}",
                        ImageCount = 4,
                        Speed = .2f
                    }
                }
            };

            Speed = 250;
        }

        public override void OnTick(float dt)
        {
            var posDelta = Velocity * dt;

            Position += posDelta;

            if (posDelta == Vector2.Zero)
            {
                Animations["idle"].OnTick(dt, RenderPrimitive);
            }
            else
            {
                if (Velocity.LengthSquared() > Speed * Speed)
                {
                    Animations["run"].OnTick(dt, RenderPrimitive);
                }
                else
                {
                    Animations["walk"].OnTick(dt, RenderPrimitive);
                }
            }

            base.OnTick(dt);
        }
    }
}