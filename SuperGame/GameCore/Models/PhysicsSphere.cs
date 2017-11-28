namespace GameCore.Models
{
    public class PhysicsSphere : PhysicsModel
    {
        public float Radius { get; set; }

        public override void Intersection(PhysicsModel modelB)
        {
            var delta = (MapObject.Position - modelB.MapObject.Position).Length();

            var radSum = Radius;

            if (modelB is PhysicsSphere)
            {
                radSum += ((PhysicsSphere) modelB).Radius;
            }

            //if (modelB is PhysicsSphere sphere)   // c# 7.0
            //{
            //    radSum += sphere.Radius;
            //}

            if (delta >= radSum)
                return;

            CallOnIntersection(modelB);

            if (IsCollision && modelB.IsCollision)
            {
                var firstModel = IsSatatic ? modelB : this;
                var secondModel = firstModel == this ? modelB : this;

                var deltaVec = firstModel.MapObject.Position - secondModel.MapObject.Position;
                firstModel.MapObject.Position -= deltaVec / deltaVec.Length() * (delta - radSum);
            }
        }
    }
}