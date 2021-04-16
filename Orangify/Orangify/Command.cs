using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orangify
{
    public class Command : ICommand
    {
        Action<object> excecuteMethod;
        Func<object, bool> canexcecuteMethod;

        public Command(Action<object> excecuteMethod, Func<object, bool> canexcecuteMethod)
        {
            this.excecuteMethod = excecuteMethod;
            this.canexcecuteMethod = canexcecuteMethod;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            excecuteMethod(parameter);
        }
    }
}
