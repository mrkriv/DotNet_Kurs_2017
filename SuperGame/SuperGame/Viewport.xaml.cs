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
using GameCore;
using GameCore.Managers;

namespace SuperGame
{
    public partial class Viewport : UserControl
    {
        private const string saveFile = "gamedata\\save.json";
        public World World { get; set; }

        public Viewport()
        {
            InitializeComponent();

            Loaded += Viewport_Loaded;
        }

        private void Viewport_Loaded(object sender, RoutedEventArgs e)
        {
            World = new World();
            World.RenderManager = new WPFRenderMenager(MainCanvas);

            World.CreateDefaultWorld();

            World.BeginSimulation();

            Focusable = true;
            Keyboard.Focus(this);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            World.InputManager.KeyDpwn((GameCore.Key)e.Key);
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            World.InputManager.KeyUp((GameCore.Key)e.Key);
            base.OnKeyUp(e);
        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            World.LoadWorld(saveFile);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            World.Save(saveFile);
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            World.CreateDefaultWorld();
        }

        public void Close()
        {
            World.EndSimulation();
        }
    }
}
