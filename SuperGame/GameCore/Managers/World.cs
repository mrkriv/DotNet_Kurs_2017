using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using GameCore.Objects;
using GameCore.Render;

namespace GameCore.Managers
{
    public class World
    {
        private List<GameObject> objects = new List<GameObject>();
        private Thread simulationThread;
        private bool isSimulation;
        public IRenderManager RenderManager { get; set; }

        public void BeginSimulation()
        {
            simulationThread = new Thread(SimulationLoop);
            simulationThread.Start();
        }

        private void SimulationLoop()
        {
            var timer = new Stopwatch();
            var dt = 0.0f;  //float

            isSimulation = true;

            while (isSimulation)
            {
                timer.Restart();

                foreach (var gameObject in objects)
                {
                    gameObject.OnTick(dt);
                }

                if (RenderManager != null)
                {
                    RenderManager.BeginRender();
                }
                //RenderManager?.BeginRender();

                timer.Stop();
                dt = (float)timer.Elapsed.TotalSeconds;

                Thread.Sleep(16);
            }
        }

        public void EndSimulation()
        {
            isSimulation = false;
        }

        public static World LoadWorld(string file)
        {
            var world = new World();

            var obj = new MapObject();
            obj.Position = new Vector2(0, 0);
            obj.Size = new Vector2(64, 64);
            obj.World = world;

            world.objects.Add(obj);
            obj.OnAttachToWorld();

            return world;
        }

        public void Save(string file)
        {
            
        }
    }
}