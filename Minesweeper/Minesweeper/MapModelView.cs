using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Minesweeper
{
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }));
        }
    }

    public class MapViewModel : PropertyChangedBase
    {
        private int size;

        public ObservableCollection<Cell> Cells { get; set; }
        public ICommand CellClickCommand { get; set; }

        public int Size
        {
            get => size;
            set
            {
                size = value;
                OnPropertyChanged("Size");
                Rebild();
            }
        }
        
        public MapViewModel()
        {
            Cells = new ObservableCollection<Cell>();
            Size = 8;

            CellClickCommand = new ObjectCommand<Cell>(c =>
            {
                c.State = CellState.Open;
            });
        }

        private void Rebild()
        {
            Cells.Clear();

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    Cells.Add(new Cell { X = i, Y = j });
                }
            }
        }
    }
}