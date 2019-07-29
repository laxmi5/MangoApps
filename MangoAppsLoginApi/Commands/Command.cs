using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MangoAppsLoginApi.Commands
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        Action<object> executeMethod;
        Func<object, bool> canexecuteMethod;

        public Command(Action<object> executeMethod,Func<object,bool> canexecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canexecuteMethod = canexecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            executeMethod(parameter);
        }
    }
}
