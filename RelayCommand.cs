using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SV_toy1
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute)// : this(execute, null)
        {
            _execute = execute;
        }
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object parameter)//when to excute
        {
            return this._canExecute == null ? true : _canExecute();
        }

        public void Execute(object parameter)//what to excute
        {
            _execute();
        }
        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
