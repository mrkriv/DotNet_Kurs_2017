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

                    modelA.Intersection(modelB);
                }
            }
        }
    }
}