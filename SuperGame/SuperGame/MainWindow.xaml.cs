using System.Windows;

namespace SuperGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Grid.Children.Add(new Viewport());
        }
    }
}
