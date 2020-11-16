using System;
using System.Collections.Generic;
using System.Text;

namespace CipherWpf
{
    public class SimpleCommand : System.Windows.Input.ICommand
    {
            public event EventHandler<object> Executed;
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                Executed?.Invoke(this, parameter);
            }
            public event EventHandler CanExecuteChanged;
    }
}
