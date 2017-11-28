using System.Numerics;

namespace GameCore.Objects
{
    public class Bullet : MapObject
    {
        public Vector2 Direction { get; set; }
        public MapObject Owner { get; set; }
        public float Speed { get; set; }
        public float LiveTime { get; set; }

        public override void OnAttachToWorld()
        {
            base.OnAttachToWorld();
            if (RenderPrimitive != null)
            {
                RenderPrimitive.ImageName = "objects\\bullet";
                RenderPrimitive.Size = new Vector2(32, 32);
            }

            PhysicsModel.IsCollision = false;
            PhysicsModel.OnIntersection += PhysicsModel_OnIntersection;
        }

        private void PhysicsModel_OnIntersection(Models.PhysicsModel a, Models.PhysicsModel b)
        {
            if (b != Owner.PhysicsModel)
            {
                b.MapObject.IsNeedDestroy = true;
                IsNeedDestroy = true;
            }
        }

        public override void OnTick(float dt)
        {
            base.OnTick(dt);

            LiveTime -= dt;
            if (LiveTime <= 0)
            {
                IsNeedDestroy = true;
                return;
            }

            Position += Direction * Speed * dt;
        }
    }
}