using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Numerics;
using GameCore.Render;

namespace GameCore.Objects
{
    public class Player : MapObject
    {
        public Dictionary<string, Animation> Animation;

        public float Speed { get; set; }

        public Player()
        {
            Animation = new Dictionary<string, Animation>
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
            var dir = new Vector2();
            var speed = Speed;

            if (World.InputManager.IsKeyDown(Key.W))
                dir += new Vector2(0, -1);

            if (World.InputManager.IsKeyDown(Key.S))
                dir += new Vector2(0, 1);

            if (World.InputManager.IsKeyDown(Key.A))
                dir += new Vector2(-1, 0);

            if (World.InputManager.IsKeyDown(Key.D))
                dir += new Vector2(1, 0);
            
            if (World.InputManager.IsKeyDown(Key.LeftShift))
                speed *= 1.5f;

            if (dir != Vector2.Zero)
                dir = dir / dir.Length();

            var posDelta = dir * speed * dt;

            Position += posDelta;

            if (posDelta == Vector2.Zero)
            {
                Animation["idle"].OnTick(dt, RenderPrimitive);
            }
            else
            {
                if (World.InputManager.IsKeyDown(Key.LeftShift))
                {
                    Animation["run"].OnTick(dt, RenderPrimitive);
                }
                else
                {
                    Animation["walk"].OnTick(dt, RenderPrimitive);
                }
            }

            if (World.RenderManager != null)
                World.RenderManager.ActiveCamera.Position = new Vector3(Position, 0);
            
            base.OnTick(dt);
        }
    }
}