using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using GameCore.Render;

namespace SuperGame
{
    public class WPFRenderPrimitive : IRenderPrimitive
    {
        private static ResurceManager resurceManager = new ResurceManager();
        private string _imageName;
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public string ImageName
        {
            get { return _imageName; }
            set
            {
                _imageName = value;
                
                if (_imageName != null)
                    ((Rectangle) WpfImage).Fill = new ImageBrush(resurceManager.LoadTextute(ImageName));
            }
        }

        public FrameworkElement WpfImage { get; set; }

        public WPFRenderPrimitive()
        {
            WpfImage = new Rectangle();
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