using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameCore.Managers;

namespace SuperGame
{
    public partial class Viewport : UserControl
    {
        public World World { get; set; }

        public Viewport()
        {
            InitializeComponent();

            Loaded += Viewport_Loaded;
        }

        private void Viewport_Loaded(object sender, RoutedEventArgs e)
        {
            World = World.LoadWorld("");

            World.RenderManager = new WPFRenderMenager(MainCanvas);

            World.BeginSimulation();
        }
    }
}
