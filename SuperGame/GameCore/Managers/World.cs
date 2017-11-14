using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading;
using GameCore.Models;
using GameCore.Objects;
using GameCore.Render;
using Newtonsoft.Json;

namespace GameCore.Managers
{
    public class World
    {
        private readonly List<GameObject> objects;
        private Thread simulationThread;
        private bool isSimulation;
        public IRenderManager RenderManager { get; set; }
        public InputManager InputManager { get; set; }

        public World()
        {
            InputManager = new InputManager();
            objects = new List<GameObject>();
        }

        public void BeginSimulation()
        {
            simulationThread = new Thread(SimulationLoop);
            simulationThread.Start();
        }

        private void SimulationLoop()
        {
            var timer = new Stopwatch();
            var dt = 0.0f; //float

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

                Thread.Sleep(16);

                timer.Stop();
                dt = (float) timer.Elapsed.TotalSeconds;
            }
        }

        public void EndSimulation()
        {
            isSimulation = false;
        }

        public void LoadWorld(string file)
        {
            objects.Clear();

            if (File.Exists(file))
            {
                var jsonData = File.ReadAllText(file);
                var saveModel = JsonConvert.DeserializeObject<SaveModel>(jsonData);

                foreach (var gameObject in saveModel.Objects)
                {
                    AddObject(gameObject);
                }
            }
        }

        public void AddObject(GameObject gameObject)
        {
            gameObject.World = this;
            objects.Add(gameObject);
            gameObject.OnAttachToWorld();
        }

        public void Save(string file)
        {
            var saveModel = new SaveModel
            {
                Objects = objects.Select(x => x as MapObject).Where(x => x != null).ToList(),
                Date = DateTime.Now
            };

            var jsonData = JsonConvert.SerializeObject(saveModel);
            File.WriteAllText(file, jsonData);
        }
    }
}