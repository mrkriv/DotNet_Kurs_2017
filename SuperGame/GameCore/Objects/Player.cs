using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Numerics;
using GameCore.Render;

namespace GameCore.Objects
{
    public class Player : Character
    {
        private Vector2 lastDir = new Vector2(1, 0);

        public override void OnTick(float dt)
        {
            var dir = new Vector2();
            var speed = Speed;

            if (World.InputManager.IsKeyDown(Key.Space))
            {
                var bullet = new Bullet
                {
                    Position = Position + new Vector2(40, -30),
                    Direction = lastDir,
                    LiveTime = 1.5f,
                    Owner = this,
                    Speed = 500,
                };
                World.AddObject(bullet);
            }
            
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
            {
                dir = dir / dir.Length();
                lastDir = dir;
            }

            Velocity = dir * speed;

            base.OnTick(dt);

            if (World.RenderManager != null)
                World.RenderManager.ActiveCamera.Position = new Vector3(Position, 0);
        }
    }
}