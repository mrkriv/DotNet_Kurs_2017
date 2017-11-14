using System;
using System.Windows;

namespace SuperGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Viewport viewport;

        public MainWindow()
        {
            InitializeComponent();

            viewport = new Viewport();
            Grid.Children.Add(viewport);
        }

        protected override void OnClosed(EventArgs e)
        {
            viewport.Close();
            base.OnClosed(e);
        }
    }
}
