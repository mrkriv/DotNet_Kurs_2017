using System;
using System.Collections.Generic;
using GameCore.Models;

namespace GameCore.Managers
{
    public class Physics
    {
        public List<PhysicsModel> Models { get; set; }

        public Physics()
        {
            Models = new List<PhysicsModel>();
        }

        public void OnTick(float dt)
        {
            foreach (var modelA in Models)
            {
                foreach (var modelB in Models)
                {
                    if(modelA == modelB)
                        continue;

                    if (modelA.IsSatatic && modelB.IsSatatic)
                        continue;

                    var delta = (modelA.MapObject.Position - modelB.MapObject.Position).Length();
                    var radSum = modelA.Radius + modelB.Radius;

                    if(delta >= radSum)
                        continue;

                    var firstModel = modelA.IsSatatic ? modelB : modelA;
                    var secondModel = firstModel == modelA ? modelB : modelA;

                    var deltaVec = firstModel.MapObject.Position - secondModel.MapObject.Position;
                    firstModel.MapObject.Position -= deltaVec / deltaVec.Length() * (delta - radSum);
                }
            }
        }
    }
}