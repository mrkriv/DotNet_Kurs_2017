using System.Numerics;

namespace GameCore.Objects
{
    public class Player : MapObject
    {
        public float Speed { get; set; } = 150;

        public override void OnTick(float dt)
        {
            var dir = new Vector2();

            if (World.InputManager.IsKeyDown(Key.W))
                dir += new Vector2(0, -1);

            if (World.InputManager.IsKeyDown(Key.S))
                dir += new Vector2(0, 1);

            if (World.InputManager.IsKeyDown(Key.A))
                dir += new Vector2(-1, 0);

            if (World.InputManager.IsKeyDown(Key.D))
                dir += new Vector2(1, 0);

            Position += dir * Speed * dt;

            base.OnTick(dt);
        }
    }
}