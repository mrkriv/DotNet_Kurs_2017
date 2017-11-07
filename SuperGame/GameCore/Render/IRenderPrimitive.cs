using System.Net.Mime;
using System.Numerics;

namespace GameCore.Render
{
    public interface IRenderPrimitive
    {
        Vector2 Position { get; set; }
        Vector2 Size { get; set; }
        string ImageName { get; set; }

        void Render(Camera camera);
    }
}