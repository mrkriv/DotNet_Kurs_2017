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
        private readonly List<GameObject> needAtachObjects;
        private readonly List<GameObject> objects;
        private Thread simulationThread;
        private bool isSimulation;
        public IRenderManager RenderManager { get; set; }
        public InputManager InputManager { get; set; }
        public Physics PhysicsManager { get; set; }

        public World()
        {
            InputManager = new InputManager();
            needAtachObjects = new List<GameObject>();
            objects = new List<GameObject>();
            PhysicsManager = new Physics();
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

                PhysicsManager.OnTick(dt);

                foreach (var gameObject in objects)
                {
                    gameObject.OnTick(dt);
                }

                if (RenderManager != null)
                {
                    RenderManager.RunInUIThread(RemoveAllDestroyObject);
                    RenderManager.RunInUIThread(AttachAllUnattachObject);
                    RenderManager.BeginRender();
                }
                else
                {
                    RemoveAllDestroyObject();
                    AttachAllUnattachObject();
                }

                Thread.Sleep(16);

                timer.Stop();
                dt = (float) timer.Elapsed.TotalSeconds;
            }
        }

        private void AttachAllUnattachObject()
        {
            foreach (var gameObject in needAtachObjects)
            {
                gameObject.OnAttachToWorld();
                objects.Add(gameObject);
            }

            needAtachObjects.Clear();
        }

        private void RemoveAllDestroyObject()
        {
            var toDestroy = objects.Where(x => x.IsNeedDestroy).ToList();

            foreach (var gameObject in toDestroy)
            {
                RemoveObject(gameObject);
            }
        }

        public void EndSimulation()
        {
            isSimulation = false;
        }

        public void LoadWorld(string file)
        {
            ClearWorld();

            if (File.Exists(file))
            {
                var jsonOpt = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };

                var jsonData = File.ReadAllText(file);
                var saveModel = JsonConvert.DeserializeObject<SaveModel>(jsonData, jsonOpt);

                foreach (var gameObject in saveModel.Objects)
                {
                    AddObject(gameObject);
                }
            }
        }

        public void CreateDefaultWorld()
        {
            ClearWorld();

            AddObject(new Terrain
            {
                Name = "Земля"
            });

            AddObject(new MapObject
            {
                Size = new Vector2(128, 128),
                Position = new Vector2(150, 200),
                ImageName = "objects\\topdownTile_31",
                Name = "Куст"
            });

            AddObject(new Player
            {
                Size = new Vector2(128, 128),
                Position = new Vector2(0, 0),
                Name = "PlayerCharacter"
            });

            AddObject(new NPC
            {
                Size = new Vector2(128, 128),
                Position = new Vector2(200, -150),
                Name = "NPC1"
            });
        }

        public void ClearWorld()
        {
            while (objects.Any())
            {
                RemoveObject(objects.First());
            }
        }

        public void AddObject(GameObject gameObject)
        {
            gameObject.World = this;
            if (simulationThread == Thread.CurrentThread)
            {
                needAtachObjects.Add(gameObject);
            }
            else
            {
                objects.Add(gameObject);
                gameObject.OnAttachToWorld();
            }
        }

        public void RemoveObject(GameObject gameObject)
        {
            gameObject.OnDetach();
            objects.Remove(gameObject);
            gameObject.World = null;
        }

        public void Save(string file)
        {
            var saveModel = new SaveModel
            {
                Objects = objects,
                Date = DateTime.Now
            };

            var jsonOpt = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            };

            var jsonData = JsonConvert.SerializeObject(saveModel, jsonOpt);
            File.WriteAllText(file, jsonData);
        }
    }
}