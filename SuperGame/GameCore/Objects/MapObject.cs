using System.Numerics;
using GameCore.Render;

namespace GameCore.Objects
{
    public class MapObject : GameObject
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public string ImageName { get; set; }

        public IRenderPrimitive RenderPrimitive { get; set; }

        public override void OnAttachToWorld()
        {
            if (World.RenderManager != null)
            {
                RenderPrimitive = World.RenderManager.CreatePrimitive();

                RenderPrimitive.Size = Size;
                RenderPrimitive.Position = Position;
            }
        }
    }
}