using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Numerics;
using GameCore.Render;

namespace GameCore.Objects
{
    public class Player : Character
    {
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

            Velocity = dir * speed;

            base.OnTick(dt);

            if (World.RenderManager != null)
                World.RenderManager.ActiveCamera.Position = new Vector3(Position, 0);
        }
    }
}