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

namespace Minesweeper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int Size = 8;
        private const int MineCount = 2;
        private bool[,] map;

        private int trueSelectCount;
        private int selectCount;

        public MainWindow()
        {
            InitializeComponent();

            Reset();
        }

        private void Reset()
        {
            trueSelectCount = 0;
            selectCount = 0;

            CreateMine();
            ClearCell();
            CreateCell();
        }

        private void CreateMine()
        {
            var rand = new Random();

            map = new bool[Size, Size];

            var count = 0;
            while(count != MineCount)
            {
                var x = rand.Next(Size);
                var y = rand.Next(Size);

                if(map[x,y] == false)
                {
                    map[x, y] = true;
                    count++;
                }
            }
        }

        private void CreateCell()
        {
            for (int i = 0; i < Size; i++)
            {
                game_map.RowDefinitions.Add(new RowDefinition());
                game_map.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    var btn = new CellControl(i, j);
                    btn.OnOpenCell += OnOpenCell;
                    btn.OnSelectCell += OnSelectCell;
                    btn.OnUnselectCell += OnUnselectCell;
                    
                    game_map.Children.Add(btn);

                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);
                }
            }
        }

        private void OnSelectCell(CellControl sender)
        {
            if (map[sender.X, sender.Y])
                trueSelectCount++;

            selectCount++;

            CheckVin();
        }

        private void OnUnselectCell(CellControl sender)
        {
            if (map[sender.X, sender.Y])
                trueSelectCount--;

            selectCount--;

            CheckVin();
        }

        private void CheckVin()
        {
            if(selectCount == MineCount && selectCount == trueSelectCount)
            {
                MessageBox.Show("Eeee!");
                Reset();
            }
        }

        private void OnOpenCell(CellControl sender)
        {
            if(map[sender.X, sender.Y])
            {
                MessageBox.Show("Mine!");
                Reset();
                return;
            }

            int count = 0;
            ForeachCell(sender, (x, y) =>
            {
                if (map[x, y])
                    count++;
            });

            sender.SetMineCount(count);

            if(count == 0)
            {
                ForeachCell(sender, (x, y) => FindCell(x, y).OpenCell());
            }
        }

        private void ForeachCell(CellControl CenterCell, Action<int, int> Action)
        {
            for (int x = CenterCell.X - 1; x <= CenterCell.X + 1; x++)
            {
                for (int y = CenterCell.Y - 1; y <= CenterCell.Y + 1; y++)
                {
                    if (x < 0 || y < 0 || x >= Size || y >= Size)
                        continue;

                    Action.Invoke(x, y);
                }
            }
        }

        private CellControl FindCell(int x, int y)
        {
            foreach (CellControl cell in game_map.Children)
            {
                if (cell.X == x && cell.Y == y)
                    return cell;
            }

            return null;
        }

        private void ClearCell()
        {
            game_map.Children.Clear();

            game_map.RowDefinitions.Clear();
            game_map.ColumnDefinitions.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }
    }
}
