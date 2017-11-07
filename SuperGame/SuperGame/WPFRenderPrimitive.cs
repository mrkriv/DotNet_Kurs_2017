using System.Numerics;
using System.Windows.Controls;
using GameCore.Render;

namespace SuperGame
{
    public class WPFRenderPrimitive : IRenderPrimitive
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public string ImageName { get; set; }

        public Image WpfImage { get; set; }

        public WPFRenderPrimitive()
        {
            WpfImage = new Image();
        }

        public void Render(Camera camera)
        {
            var position = camera.Resolution / 2 - new Vector2(camera.Position.X, camera.Position.Y);
            position += Position;

            var size = Size;// * camera.Position.Z;

            Canvas.SetLeft(WpfImage, position.X);
            Canvas.SetTop(WpfImage, position.Y);

            WpfImage.Width = size.X;
            WpfImage.Height = size.Y;
        }
    }
}