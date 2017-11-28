using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using GameCore.Render;

namespace GameCore.Objects
{
    public class Terrain : GameObject
    {
        private IRenderPrimitive[,] primitives = new IRenderPrimitive[TerrainSize, TerrainSize];
        private const int TerrainCellSize = 128;
        private const int TerrainSize = 16;

        public List<string> Map { get; set; }

        public Terrain()
        {
            Map = new List<string>();
        }

        public override void OnAttachToWorld()
        {
            if (Map.Count == 0)
            {
                for (var i = 0; i < TerrainSize; i++)
                {
                    var row = "";
                    for (var j = 0; j < TerrainSize; j++)
                    {
                        row += "25 ";
                    }
                    Map.Add(row);
                }
            }

            if (World.RenderManager != null)
            {
                var offset = new Vector2(TerrainCellSize * TerrainSize, TerrainCellSize * TerrainSize);
                offset /= 2;

                for (var i = 0; i < TerrainSize; i++)
                {
                    var mapRow = Map[i].Split(' ');
                    for (var j = 0; j < TerrainSize; j++)
                    {
                        var cell = World.RenderManager.CreatePrimitive();
                        cell.Size = new Vector2(TerrainCellSize + 1, TerrainCellSize + 1);
                        cell.Position = new Vector2(TerrainCellSize * i, TerrainCellSize * j) - offset;

                        cell.ImageName = "terrain\\topdownTile_" + mapRow[j];

                        primitives[i, j] = cell;
                    }
                }
            }

            base.OnAttachToWorld();
        }

        public override void OnDetach()
        {
            if (World.RenderManager != null)
            {
                for (var i = 0; i < TerrainSize; i++)
                {
                    for (var j = 0; j < TerrainSize; j++)
                    {
                        World.RenderManager.DestroyPrimitive(primitives[i, j]);
                    }
                }
            }

            base.OnDetach();
        }
    }
}