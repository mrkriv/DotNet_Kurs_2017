using System;
using System.Windows.Input;

namespace Minesweeper
{
    public class ObjectCommand<T> : ICommand where T : class
    {
        private readonly Action<T> action;

        public event EventHandler CanExecuteChanged;

        public ObjectCommand(Action<T> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action?.Invoke(parameter as T);
        }
    }
}