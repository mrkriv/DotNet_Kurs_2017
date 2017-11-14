using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using GameCore.Render;

namespace SuperGame
{
    public class WPFRenderMenager : IRenderManager
    {
        private readonly List<WPFRenderPrimitive> primitives = new List<WPFRenderPrimitive>();

        public Camera ActiveCamera { get; set; }
        public Canvas WpfCanvas { get; set; }

        public WPFRenderMenager(Canvas wpfCanvas)
        {
            ActiveCamera = new Camera();
            WpfCanvas = wpfCanvas;
        }

        public IRenderPrimitive CreatePrimitive()
        {
            var primitive = new WPFRenderPrimitive();
            WpfCanvas.Children.Add(primitive.WpfImage);
            primitives.Add(primitive);

            return primitive;
        }

        public void DestroyPrimitive(IRenderPrimitive primitive)
        {
            //var wpfRenderPrimitive = primitive as WPFRenderPrimitive;
            //if (wpfRenderPrimitive != null)
            if (primitive is WPFRenderPrimitive wpfRenderPrimitive)
            {
                WpfCanvas.Children.Remove(wpfRenderPrimitive.WpfImage);
                primitives.Remove(wpfRenderPrimitive);
            }
        }

        public void BeginRender()
        {
            if(ActiveCamera == null)
                return;

            WpfCanvas.Dispatcher.BeginInvoke((ThreadStart) delegate
            {
                ActiveCamera.Resolution = new Vector2((float) WpfCanvas.ActualWidth, (float) WpfCanvas.ActualHeight);

                foreach (var primitive in primitives)
                {
                    primitive.Render(ActiveCamera);
                }
            });
        }
    }
}
